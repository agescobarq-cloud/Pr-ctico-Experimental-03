# Sistema de Registro de Libros - Biblioteca

**Práctica #03 - Estructura de Datos**  
**Universidad Estatal Amazónica**  
**Asignatura:** Estructura de Datos (UEA-L-UFB-032)  
**Unidad:** Conjuntos y Mapas  
**Lenguaje:** C#

---

## Descripción del Proyecto

Aplicación de consola desarrollada en **C#** para el registro, organización y consulta de libros en una biblioteca.  

El sistema utiliza de manera prioritaria **conjuntos (`HashSet`)** y **mapas/diccionarios (`Dictionary`)** para gestionar la información de forma eficiente, garantizando unicidad y acceso rápido a los datos.

---

## Características Principales

- Registro de libros con ISBN, título, autor, género, año y ejemplares disponibles
- Búsqueda rápida por ISBN
- Listado completo de libros
- Filtrado de libros por género
- Visualización de autores únicos
- Visualización de géneros únicos
- Conteo de libros por género
- Búsqueda de libros por autor
- Análisis de tiempo de ejecución de las estructuras de datos

---

## Estructuras de Datos Utilizadas

| Estructura                        | Tipo en C#                          | Propósito                                      |
|-----------------------------------|-------------------------------------|------------------------------------------------|
| Mapa principal                    | `Dictionary<string, Libro>`         | Almacenar libros indexados por ISBN            |
| Conjunto de autores               | `HashSet<string>`                   | Control de unicidad de autores                 |
| Conjunto de géneros               | `HashSet<string>`                   | Control de unicidad de géneros                 |
| Contador por género               | `Dictionary<string, int>`           | Cantidad de libros por cada género             |
| Agrupación por autor              | `Dictionary<string, List<Libro>>`   | Consulta rápida de libros de un autor          |

---
