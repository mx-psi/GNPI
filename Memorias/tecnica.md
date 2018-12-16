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
Hemos implementado el proyecto utilizando la versión 5.6.6f2 Personal de Unity.

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

- un componente *Hand Controller (Script)* que controla la mano. En todas las escenas este componente tiene un script asociado del plugin de Leap que controla el movimiento básico de la mano y su reconocimiento.
- Un componente *Gestures Controller (Script)* descrito en el script `GesturesController`.

Además, omitimos la descripción de los renderers o colliders cuando no sean necesarios para la explicación técnica del funcionamiento de la escena.

## `mainScene`

La escena principal consta de los objetos por defecto así como lo siguientes elementos:

Mapa
: El mapamundi, con el material asociado `Mapamundi` que nos muestra una versión simplificada del mapa del mundo

*Selectable Items*
: Cápsulas que nos permiten acceder al resto de escenas.
  Cada escena tiene un *Capsule Collider* que nos permite interactuar con las mismas.
  Además, el componente *Selectable Object (Script)* tiene asociada la escena a cargar y la animación asociada (ver documentación en [Scripts]).
UI
: Tiene los componentes relativos a la interfaz de usuario, con los que no se puede interactuar.

## `musicScene`

La escena de música consta de los objetos principales además de los siguientes dos objetos:

`Bongo left` y `bongo right`
: Estos objetos tienen asociados:

- Un *Rigid body* y un *Capsule collider* para calcular las colisiones y poder tocar los bongos,
- Un *audioSource* para indicar el sonido a reproducir cuando se toque el bongo,
- Un script *bongoScript* que gestiona cuándo se considera que se ha tocado el bongo (ver sección scripts para documentación),


## `paintingScene`

La escena de creación de lienzos consta de los objetos principales además de los siguientes objetos:

- `MainCanvas` y `EventSystem` para las animaciones. Su script asociado es `canvasTimerDisable`,
- `Trash can` y `Non alpha affected canvas` para el cubo de basura. Su script asociado es `selectableObject`,
- `PaintingScene Controller` y los `Paintable Quad` generados para el lienzo dinámicamente por el script `gridScript`.

## `sculptureScene`

La escena de visión de escultura consta de los objetos principales además de los siguientes objetos:

- `venus`, `pensador`, `urinal` y `david` son los objetos visualizables. Tienen asociados un `RigidBody` para físicas y un script `GrabbableObject` de los plugin de Leap para la interacción con Leap
- `Base & bounding box` es la caja en la que se encuentran los objetos. El suelo es visible pero las paredes y el techo no.
  Constan de collider para evitar que el objeto se salga fuera de la región visible.

# Scripts

Todas las clases que hemos implementado heredan de `MonoBehaviour`,
por lo que consideramos que no es necesario hacer un diagrama de clases.

## Documentación de clases

La clase `MonoBehaviour` requiere de la implementación del método de inicialización `Start` y del método `Update`.
Si en algún caso alguno de estos métodos no tiene una función especial más allá de la inicialización o están vacíos se omiten de la documentación de ese script.

### `GesturesController`

Script asociado a la detección de gestos de la mano.

Los atributos de la clase son:

- `controller` El controlador asociado a Leap,
- `zoom` y `movingHands`, que indican el modo actual (si se está haciendo zoom o haciendo el gesto de mover la mano),
- `distBase` y `distRel`
- `suavizadoZoom` parámetro para la función de suavizado del zoom y
- `camera_` y `posCamara` que son la cámara y su posición en la escena actual.

La clase tiene los siguientes métodos para realizar las acciones asociadas a cada gesto:

- `MapNavigation` para los gestos de desplazarse por el mapa,
- `Next` para mostrar el siguiente modelo o cambiar al siguiente color, según la escena,
- `Previous` para mostrar el anterior modelo o cambiar al anterior color, según la escena,
- `Zoom` para cambiar la posición de la cámara al reconocer el gesto del zoom,
- `Exit` para salir a la escena principal desde cualquiera de las otras escenas.

El método que se encarga de reconocer cada gesto es `Update`. Este método se llama en cada frame y verifica si se cumplen las condiciones que activan cada uno de los gestos usando datos como `isExtended` de la librería de `Leap` que informa de si un dedo concreto está extendido o `StabilizedPalmPosition`, de la misma librería, que indica la posición de la palma de la mano corrigiéndola para darle estabilidad.

### `selectableObject`

Este script está asociado a las cápsulas seleccionables en la escena principal.

Los atributos de la clase son:

- `transf` es la `Transformation` actual de la cápsula,
- `sceneToLoad` es el nombre de la escena a cargar,
- `anim` es la animación asociada a la carga,
- `startTimer` es el último tiempo de comienzo del contador de colisión,
- `loadingTime` es el tiempo que debe mantenerse la colisión para la carga,
- `totalCollisions` es el número de colisiones detectadas (0 o 1).

Los métodos de la clase son:

- `Update` anima la cápsula para que rote.
- `onTriggerEnter` se activa cuando se detecta colisión. Comienza el contador y la animación de carga.
- `onTriggerStay` carga la escena correspondiente si el tiempo de colisión supera al tiempo fijado (por defecto 2 segundos)
- `OnTriggerExit` anula la carga si la colisión no supera el tiempo fijado.

## `bongoScript`

Este script gestiona la detección del tocado de los bongos.

Tiene un único atributo `audioData` correspondiente al audio a reproducir con la colisión de los bongos.

Tiene un único método relevante, `OnCollisionEnter`, que reproduce el audio si se detecta una colisión con una palma.

## `gridScript`

Este script genera dinámicamente el lienzo en el que se pinta a partir de cuadrados pintables prefabricados.

El único método relevante es `createGrid`, que crea el lienzo instanciando cuadrados pintables en las posiciones adecuadas.

## `canvasTimerDisableScript`

Este script desactiva una animación.

El método `Update` aumenta progresivamente la transparencia del objeto.


# Recursos externos utilizados

## Modelos 3D y texturas

El mapamundi se ha obtenido de [ClipArtBest](http://www.clipartbest.com/clipart-niEnp5BiA)

El  modelo del cubo de basura se ha obtenido de [Free3D.com](https://free3d.com/3d-model/trash-can-v3--841062.html).

Las figuras 3D de obras de arte se han obtenido de [Scan The World](https://www.myminifactory.com/es/scantheworld/).
Las hemos tratado utilizando Blender para reducir el número de vértices.

El resto de iconos, interfaces, logos y animaciones se han producido por el propio equipo



## Sonidos

Los sonidos de los bongos se han obtenido del repositorio:

<https://github.com/Externalizable/bongo.cat>


## Recursos de Leap

Los recursos de Leap se han obtenido del siguiente repositorio de Github:

<https://github.com/leapmotion/LeapMotionCoreAssets>

## Otros recursos

Hemos hecho uso de la documentación de Unity y la documentación de la API de Leap en C# para aprender cómo utilizar Unity y Leap.
