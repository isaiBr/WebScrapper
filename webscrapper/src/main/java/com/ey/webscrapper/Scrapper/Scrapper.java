package com.ey.webscrapper.Scrapper;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

@Service
public class Scrapper {
    public void encontrarEntidadesAltoRiesgo (String nombre){
        /*
    List<String> fuentes = new ArrayList<>(Arrays.asList(
            "",
            "",
            ""));
     */

        String nombreBusqueda = transformarNombreCadena(nombre);
        String fuente = "https://offshoreleaks.icij.org/search?q="+nombreBusqueda;

        try{
            Document document = Jsoup.connect(fuente).get();
            System.out.println(document);
            Element tablaEntidades = document.select("table.table.table-sm.table-striped.search__results__table").first();
            if(tablaEntidades!=null){
                Elements entidades = tablaEntidades.select("tbody tr");

                for(Element entidad : entidades){
                    String entity = entidad.select("td > a").text();
                    String jurisdiction = entidad.select("td.jurisdiction").text();
                    String linkedTo = entidad.select("td.country").text();
                    String dataFrom = entidad.select("td.source.text-nowrap a").text();

                    System.out.println(entity + " | "+ jurisdiction + " | "+linkedTo+ " | " + dataFrom);
                }
            }
            else{
                System.out.println("Error con la tabla de elementos");
            }

        }catch(IOException e){
            e.printStackTrace();
        }

    }

    /*
    public void entitiesHighRisk (String nombre){
        WebDriverManager.chromedriver().setup();
        ChromeOptions options = new ChromeOptions();
        options.addArguments("--headless");

        try (WebDriver driver = new ChromeDriver(options)) {
            String fuente = "https://offshoreleaks.icij.org/search?q=" + nombre;
            driver.get(fuente);

            // Espera hasta que la página se cargue completamente
            // Aquí puedes ajustar el tiempo de espera según sea necesario
            Thread.sleep(5000);

            List<WebElement> entidades = driver.findElements(By.cssSelector("table.table.table-sm.table-striped.search__results__table tbody tr"));

            for (WebElement entidad : entidades) {
                String entity = entidad.findElement(By.cssSelector("td > a")).getText();
                String jurisdiction = entidad.findElement(By.cssSelector("td.jurisdiction")).getText();
                String linkedTo = entidad.findElement(By.cssSelector("td.country")).getText();
                String dataFrom = entidad.findElement(By.cssSelector("td.source.text-nowrap a")).getText();

                System.out.println(entity + " | " + jurisdiction + " | " + linkedTo + " | " + dataFrom);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


     */
    private String transformarNombreCadena(String cadena){
        return cadena.replace(" ", "+");
    }


}
