package com.ey.webscrapper.Controller;

import com.ey.webscrapper.Scrapper.Scrapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/scraper")
public class ScrapperController {
    @Autowired
    private Scrapper scrapper;

    @GetMapping("/saludar")
    public String holaMundo(){
        return "Realizando mi controlador";
    }

    @GetMapping("/entidades/{nombre}")
    public void getEntidades(@PathVariable("nombre") String nombre){
        scrapper.encontrarEntidadesAltoRiesgo(nombre);
    }


}
