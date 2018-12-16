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

El proyecto consta de 4 escenas:

`mainScene.unity`
: La escena principal. Esta escena muestra el mapamundi, la ayuda y permite acceder al resto de escenas.
  Su uso se describe en la sección "El mapamundi" de la memoria descriptiva.
  
`musicScene.unity`
: Escena musical. Esta escena muestra dos bongos y permite tocarlos con las palmas de las manos.
  Su uso se describe en la sección "Música" de la memoria descriptiva.
  
`paintingScene.unity`
: Escena de lienzo. Esta escena muestra un lienzo y un cubo de basura. 
  Permite al usuario pintar utilizando sus manos. Su uso se describe en la sección "Creación de cuadros" de la 
  memoria descriptiva.

`sculptureScene.unity`
: Escena de visualización de obras de arte. Esta escena muestra una obra de arte y permite interactuar con ella.
  Su uso se describe en la sección "Interacción con obras de arte" de la memoria descriptiva.

A continuación se describe la implementación de cada escena.
Todas las escenas contienen los siguientes objetos (con las propiedades por defecto salvo que se indique lo contrario):

`MainCamera`
: Cámara por defecto. En algunas escenas tiene un script que maneja el comportamiento de parte de la escena.

`DirectionalLight`
: Luz por defecto

`HandControllerSandbox`
: Consta de:

- un componente *Hand Controller (Script)* que controla la mano. En todas las escenas este componente tiene un script asociado del plugin de Leap [TODO asegurarse que es del plugin] que controla el movimiento básico de la mano y su reconocimiento.
- Un componente *Gestures Controller (Script)* descrito en el script TODO.

Además, omitimos la descripción de los renderers o colliders cuando no sean necesarios para la explicación técnica del funcionamiento de la escena.

## `mainScene`

La escena principal consta de los objetos por defecto así como lo siguientes elementos:

Mapa
: El mapamundi, con un material *Mapamundi* que nos muestra una versión simplificada del mapa del mundo.

*Selectable Items*
: Cápsulas que nos permiten acceder al resto de escenas.
  Cada escena tiene un *Capsule Collider* que nos permite interactuar con las mismas.
  Además, el componente *Selectable Object (Script)* tiene asocada la escena a cargar y la animación asociada
  
UI
: Tiene los componentes relativos a la interfaz de usuario, con los que no se puede interactuar.

## `musicScene`

La escena de música consta de los objetos principales además de los siguientes dos objetos:

`Bongo left` y `bongo right`
: Estos objetos tienen asociados:

- Un *Rigid body* y un *Capsule collider* para calcular las colisiones y poder tocar los bongos,
- Un *audioSource* para indicar el sonido a reproducir cuando se toque el bongo,
- Un script *bongoScript* que gestiona cuándo se considera que se ha tocado el bongo


## `paintingScene`

## `sculptureScene`


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
