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
- Intento añadir los controles de Nuget, para unos componentes muy útiles para el programa pero no lo conseguí, por muchos tutoriales que seguí

## Día 4
> 18/08/2021
- Cambio del formulario que hice ayer, puesto que lo cree como página y no como ventana
- Colocación de los componentes para seleccionar el color del nuevo formulario.
- Cambio de la pagina wpf de nuveoDocumento por una ventana wpf

## Día 5
> 19/08/2021
- Solucionando un error de NullReferecnceException del slider si lo inicio a su valor máximo
- Lo solucioné iniciando el contructor con los valores de los sliders a su valor máximo, pero al iniciar, la barra de valores roja se miraba verde. 
- Todo fue perder el tiempo, no me di cuenta de algo tan básico en el color, de que no es lo mismo (255, 0 , 0) que (0, 255, 255)

## Día 6
> 25/08/2021
- Creación del canvas con los ajustes establecidos
- Añadido un check para habilitar o deshabilitar la elección del color del canvas
- DialogResult para los botones de aceptar y cancelar

## Día 7
> 26/08/2021
- Comentarios de algunas funciones
- Cración de la calse Pincel para no tener tanto código en el principal
- Prueba del pincel en el canvas, exitosa, pendiente de algunos ajustes de fluided

## Día 8
> 31/08/2021
- Arreglado que cuando abramos un lienzo nuevo se ponga sobre el anterior
- Guardar la imagen de un canvas en un archivo png o jpg

## Día 9
> 02/09/2021
- Cambiado el Canvas para dibujar por un InkCanvas 
- Borrado de la mayor parte del código que no era útil para este nuevo componente
- Nueva función de guardar
- Creada también la de Cargar
- Arreglado el fallo de que se mantenga lo pintado al abrir imagen o nuevo lienzo

## Día 10
> 06/09/2021
- Después de todo un día de búsqueda conseguí que al guardar el InkCanvas en una imagen, salga desplazado

## Día 11
> 07/09/2021
- Ajustes de pincel, y botón para el mismo
- Goma de borrar funcionable
- Herramienta de selección lista, todas estas herramientas vienen con el InkCanvas

## Día 11
> 08/09/2021
- Busqueda de como hacer una herramienta de relleno, por todo lo que encontré, no se puede hacer con InkCanvas, pero alguien hablaba de pasar la imagen a BitMap y trabajar con las coordenadas, lo dejaré entonces para más adelante y así avanzar con el proyecto
- Inicio de una forma personalizada básica para ver como trabaja con ella el inkCanvas

## Dia 12
> 9/09/2021
- Después de mucho buscar, no descubrí como hacer unas formas personalizadas de la manera que quiero, arrastrando y colocando la figura, solo consegui colocarlas en el inkCanvas, pero no elegir de forma holgada donde se colocan, a parte, una vez colocadas no se pueden borrar, por lo que entran al dibujo a molestar. Podría cambiar el InkCanvas por un Canvas, pero me interesa la funcionalidad que tiene inkCanvas para usar con tableta. Así que haré otras funciones del programa más útiles para dibujar.


## Dia 13
> 10/09/2021
- Wpf no maneja como windows form el COlorDialog, vi varias formas de hacerlo, pero ninguna me funcionó, así que me decanté por crear el mio propio
