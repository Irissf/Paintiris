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

## Dia 14
> 12/09/2021
- El ColorDialog personal frabricado, le quedan algunos detalles que arreglaré por la semana, pero en principio funciona, tuve problemas con las transparecencias ya que no me fijé donde se colocaba, pensando que era el último parámetro cuando en realidad era el primero.

## Dia 15
> 13/09/2021
- Cambio de los botones normales por ToggleButton, para que quede seleccionado el botón elegido, y programado que cuando le demos a uno se desactiven los otros de manera que la goma y el pincel no estén a la vez activados.
- Arreglado el fallo de que el pincel no actualice el color cuando se cambia de color.
- Ahora cuando se le da al canvas del color para cambiarlo, manda al formulario modal es color que estaba para trabajar sobre él.

## Dia 15
> 14/09/2021
- Colocado un scroll para el canvas por si cambia el tamaño de la pantalla, y lo centre en pantalla adecuadamente mediante el código axml, a parte eliminé una división del grid donde está en inkcanvas que era innecesario.

## Dia 16
> 16/09/2021
- Modos de poner imágenes en los iconos, o bien mediante una geometría o una imagen descargada, pero este último su calidad no se ve óptima

## Dia 17
> 21/09/2021
- Trabajando en mejorar la parte gráfica de los pinceles, añadiendo botones para el tamaño de los pinceles.

## Dia 18
> 22/09/2021
- Programados los botones de los tamaños de los pinceles, luego tengo que mandar a una función la gestión de pintar para que no se repita tanto código cada vez que se hace un cambio en el pincel
- Programados los botones de pincel o lápiz, con lápiz, el ancho y alto son iguales, pero con pincel el ancho es la mitad del alto para simular un óvalo.
- Arreglado el error del que al cambiar el color cambie el tamaño del pincel con unas variables comunes, puesto que las tenia dentro de la función no compartían los datos.
- Solucionado un error que aparecia al pulsar el lápiz o el pincel puesto que partía con un valor nulo, lo solucioné poniendo un valor inicial a las variables de un px


## Dia 19
> 24/09/2021
- Gestionados los tamaños de un botón, de forma que no estén activados si no está una herramienta de dibujo seleccionada.
- Cambiar la forma de coger el tamaño del pincel, quitamos el switch-case y lo hacemos mediante el tag del componente.
- Cambiamos de nuevo el tamaño por defecto del pincel y controlamos que el valor nunca sea menor de uno. Eliminamos una excepción de tipo ArgumentOutOfRangeException, la cual aparecia con el pincel de tamaño 1, puesto que la mitad es 0.5 y al redondear en int, mandaba el valor de 0, lo controlamos mediante un if que indica que si un valor es menor a uno lo ponemos a uno.
- Colocados los cambas de la paleta de colores.
- Programados los canvas de la paleta de colores, de forma que si das click derecho cambias el color del mismo, y con click izquierdo lo seleccionas para usar el color.
