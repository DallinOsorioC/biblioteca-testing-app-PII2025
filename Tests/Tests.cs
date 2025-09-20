using App.Entidades;

namespace Tests
{
    public class Tests
    {
        private Biblioteca _biblioteca;
        private Libro _libro1;
        private Libro _libro2;

        [SetUp]
        public void Setup()
        {
            _biblioteca = new Biblioteca();
            _libro1 = new Libro("1984", "George Orwell");
            _libro2 = new Libro("El Principito", "Antoine de Saint-Exupéry");
            _biblioteca.AgregarLibro(_libro1);
            _biblioteca.AgregarLibro(_libro2);
        }

        [Test]
        public void PrestarLibro_LibroDisponible_PrestaLibroCorrectamente()
        {
            // Act
            _biblioteca.PrestarLibro("1984");

            // Assert
           Assert.That(_libro1.EstaPrestado, Is.True);
        }

        [Test]
        public void PrestarLibro_LibroNoDisponible_LanzaExcepcion()
        {
            // Act
            var resultado = Assert.Throws<InvalidOperationException>(() => _biblioteca.PrestarLibro("test"));
            // Assert
            Assert.That(resultado.Message.ToString(), Is.EqualTo("El libro no se encuentra en la biblioteca."));
        }

        [Test]
        public void DevolverLibro_LibroPrestado_DevolveLibroCorrectamente()
        {
            // Act
            _biblioteca.PrestarLibro("1984");
            _biblioteca.DevolverLibro("1984");
            
            // Assert
            Assert.That(_libro1.EstaPrestado, Is.False);
        }

        [Test]
        public void DevolverLibro_LibroNoPrestado_LanzaExcepcion()
        {
            // Act
            var resultado = Assert.Throws<InvalidOperationException>(() => _biblioteca.DevolverLibro("1984"));
            // Assert
            Assert.That(resultado.Message.ToString(), Is.EqualTo("El libro no está prestado."));
        }

        [Test]
        public void ObtenerLibros_RetornaListaDeLibros()
        {
            // Act
            var libros = _biblioteca.ObtenerLibros();
            
            // Assert
            Assert.That(libros.Count, Is.EqualTo(2));

        }
    }
}