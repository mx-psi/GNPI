---
title: Memoria técnica de la práctica 3 
date: Nuevos Paradigmas de Interacción
author:
- Pablo Baeyens Fernández
- Francisco Javier Morales Piqueras
- Jesús Sánchez de Lechina Tejada
documentclass: scrartcl
secnumdepth: 3
lang: es
numbersections: true
linkReferences: true
colorlinks: true
toc: true
---

\newpage

# Estructura del proyecto

Musemium 2 está implementado como un proyecto para Unity apoyándose de las librerías de Leap Motion Desktop v2.
Hemos implementado el proyecto utilizando la versión TODO de Unity.

En esta sección describimos la estructura en carpetas del proyecto.
Describimos sólo la carpeta `Assets` y se ignoran los ficheros `.meta`, ya que el resto (tanto estos ficheros como la carpeta `ProyectSettings`) corresponden a configuraciones de Unity y no a código o recursos editados por nosotros.

La carpeta `Assets` tiene las siguientes carpetas, descritas en orden alfabético:

- `Gizmos` y `Plugins` contienen los ficheros necesarios para el plugin de Leap versión 2 (ver [Recursos de Leap]),
- `Musemium` contiene el código, *prefabs*, materiales, sonidos y texturas asociados al proyecto, en concreto:
   - `ArtModels` contiene los modelos 3D del visualizador 3D así como el modelo de cubo de basura del modo de pintura (ver [Modelos 3D y texturas]),
   - `Materials` contiene los materiales relativos a los distintos elementos, las texturas y los frames de las animaciones (ver [Modelos 3D y texturas]),
   - `Prefabs` contiene los *prefabs* asociados a las manos que se muestran en pantalla (ver [Recursos de Leap]),
   - `Scenes` contiene las escenas (ver [Escenas]),
   - `Scripts` contiene los scripts (ver [Scripts]) y
   - `Sounds` contiene los sonidos de la escena de bongos (ver [Sonidos])
   

# Escenas

# Scripts

## Diagrama de clases

## Documentación de clases

# Recursos externos utilizados

## Modelos 3D y texturas

El mapamundi se ha obtenido de TODO

## Sonidos
## Recursos de Leap

TODO

## Otros recursos

Hemos hecho uso de la documentación de Unity y la documentación de la API de Leap en C# para aprender cómo utilizar Unity y Leap.
