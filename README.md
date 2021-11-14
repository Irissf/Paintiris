# Paintiris
> Aplicación imitando al paint 

![image](https://user-images.githubusercontent.com/64013862/141691827-f0875ffc-e055-44f1-b3e8-5f2af156a2cc.png)

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

## Dia 19
> 27/09/2021
- Cambio los canvas de los colores por Rectangle, para hacer uso de su propiedad Stroke, que me permite poner un borde, con canvas habria que poner un componente Border conteniendo al canvas
- Preparar el Combobox en el que se mostrarán las diferentes paletas de colores con la base de datos.

## Dia 20
> 01/10/2021
- Colocación de los toggleButton para los tamaños de la goma
- Meto por código algunos datos de prueba para el ComboBox

## Dia 21
> 12/10/2021
- Base de datos SQLite
- Creamos un directorio en el proyecto para almacenar la base de datos SQLite
- Creamos la base de datos PaintirisBD con tres tablas, colores, paletas y paleta_colores que será la tabla relacional de las otras dos, que contendrán las claves foraneas
- Añadimos algunos registros para probar
- Cambiamos algunas propiedades del Paintiris.bd, como son la acción de compilación que la pasamos a "Contenido" y la copia en el directorio de salida que la ponemos como "Copiar si es posterior"
- Agregamos el app.config pulsando con el botón derecho sobre el proyecto/agregar/nuevo elementos/archivos de configuarión de aplicaciones y añadimos la cadena de texto con la que nos conectaremos a la base de datos.
- Instalamos el paquete Nuguet de SQLite 
- Programamos un poco de la clase BaseDatos para probar que se conecta, si lo hace ;D

## Día 22
> 14/10/2021
- Código programado para pasar de color hex a rgb y viceversa para que el usuario decida que método le es más cómodo para poner un colo. Ahora queda implemetarlo a los Color Dialog.

## Día 23
> 16/10/2021
- Creación de la clase Conversor para poner las operaciones de cambio de RGB a HEX y viceversa.
- Programado el ColorDialog, al poner el Hex ahora cambia el texbox y el slider en RGB
- Programado también que cuando cambies un textbox o slider del rgb se actualice el hex.

## Día 24
> 17/10/2021
- Varios cambios entre el Hex y RGB, puesto que al mover uno cambiaba el otro, pero se volvia a enviar ese cambio y creaba conflicto dejando todo a 0 al final, ahora cada componente manda cambios en el otro solo cuando tenemos el foco en él, para evitar el conflicto.
- Arreglado el fallo de que se generase un número de más al cambiar de rgb a Hex
- Programado el select de prueba de la base de datos para cambiar la paleta de colores y aprlicado en los rectagles donde se muestran los colores.

## Día 24
> 19/10/2021
- Peleando con la subconsulta y el método del selectedChanged del comboBox sin llegar a absolutamente a nada

## Día 25
> 21/10/2021
- Ya hace la consulta correctamente, pero hay un error con la segunda variable de la paleta, que ya no da resultados, el problema debe ser un string que todavía no encuentro.

## Día 26
> 22/10/2021
- Ya funcionan las diferentes paletas, el problema resultó estar en la base de datos que no se habían guardado correctamente

## Día 27
> 23/10/2021
- Diccionario de recursos del ToggleButton.
- Ajustada los estilos de los botones y toggleButton
- Controlado el error de que al cargar una imagen y dar a cancelar saliese un NullReferenceException
- Arreglado lo de que al pulsar pincel, pintase en un color que no fuese el negro.
- Arreglado que botón de pincel y goma se activasen a la vez.
- Programado el textbox del cambio de tamaño de pincel

## Día 28
> 24/10/2021
- Arreglado que el color sea negro al activar el pincel a pesar de tener un color seleccionado.
- Tamaños de la goma
- textbox de la goma
- Arreglado el cruce de tamaños entre la goma y el pincel separando las variables.

## Día 29
> 27/10/2021
- Programado el botón de guardar como, eligiendo la ruta donde se guardará el dibujo.

## Día 30
> 30/10/2021
- Añadido para guardar el archivo en png.
- Metidos los filtros de png y jpg en el SaveFileDialog.
- Buscando como poder hacer zoom y rotar el lienzo

## Día 31
> 1/11/2021
- Programada la función para la carga de los folios predefinidos para estudiar.
- Encontrado como hacer zoom al inkCanvas, y programado un botón de prueba
- Todavía buscando para rotarlo.

## Día 32
> 3/11/2021
- Preparación de hojas e iconoes en Photoshop e Illustrator.
- Colocoación en el programa de los iconos de las hojas

## Día 33
> 6/11/2021
- Metidas las hojas de cuadricula.
- Programado el slider para el zoom, y el botón para regresar al tamaño original

## Día 34
> 7/11/2021
- Añadidas las hojas y los iconos de estudio.
- Lo que queda a mayores es ir probando y arreglando errores que aparezcan.

## Día 35
> 8/11/2021
- Controlado un error en el botón de "guardar como" cuando no se seleccionaba ninguna ruta.
- Anulado el botón de guardar, si primero no se ha cargada o guadado como.
- Solucionado el problema que daba cargar y luego guardar, ya que indicaba que estaban ocupando el mismo proceso.
- Arreglado un error de la carga de imagen, que cogia la altura tanto para el ancho como para el alto.

## Día 36
> 13/11/2021
- Controlada la excepción formatException y OverFlowException de los textbox de crear nuevo documento.
- Controlada también las mismas excepciones de los texbox del tamaño del pincel y de la goma.
- Preguntar al ususario si quiere guardar el archivo.
- Arreglado un fallo del textbox del color hex para que los datos que colocoa el usuario sean los correctos.
- Arreglado que el cursor del textbox del color del hex, al arreglar el dato del ususario, colocaba el cursor antes del caracter nuevo.
- Arreglado que al cambiar de color con click derecho sobre el color no se pasaba el color.
- Botón de cancelar de ColorDialog.

## Día 37
> 14/11/2021
- Preguntar antes de salir
- Añadir a select y zoom que no estén activados a la vez que otros.
- Preguntar antes de cargar las hojas prefabricadas de estudio.
- El label de abajo ya muestra el nombre del archivo.
- Creación y colocación del icono de la aplicación.
- Botón "acerca de".
