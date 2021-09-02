# Paintiris
> Aplicación imitando al paint 
## Día 1 
> 11/08/2021 
- División del formulario principal en 3 zonas, la del menú, la central para el dibujo y la inferior para información.
- Utilización del componente Tab Item para la zona del menú.
- Prueba para añadir un lienzo, por ahora de tamaño fijo.

## Día 2
> 12/08/2021
- Creación de los diccionarios de recursos para personalizar los componentes más genéricos del proyecto
  - Button
  - TabControl
  - TabItem
- Realización de los cambios de estílo definitivos para TabControl y TabItem

## Día 3
> 17/08/2021
- Colocación de los componentes del formulario para nuevo documento de Paintiris

## Día 4
> 18/08/2021
- Colocación de los componentes para seleccionar el color del nuevo formulario.
- Cambio de la pagina wpf de nuveoDocumento por una ventana wpf

## Día 5
> 25/08/2021
- Creación del canvas con los ajustes establecidos
- Añadido un check para habilitar o deshabilitar la elección del color del canvas
- DialogResult para los botones de aceptar y cancelar

## Día 6
> 26/08/2021
- Comentarios de algunas funciones
- Cración de la calse Pincel para no tener tanto código en el principal
- Prueba del pincel en el canvas, exitosa, pendiente de algunos ajustes de fluided

## Día 7
> 31/08/2021
- Arreglado que cuando abramos un lienzo nuevo se ponga sobre el anterior.
- Guardar la imagen de un canvas en un archivo png o jpg

## Día 8
> 2/09/2021
- Cambiado el Canvas para dibujar por un InkCanvas 
- Borrado de la mayor parte del código que no era útil para este nuevo componente
- Nueva función de guardar
- Creada también la de Cargar
- Arreglado el fallo de que se mantenga lo pintado al abrir imagen o nuevo lienzo
