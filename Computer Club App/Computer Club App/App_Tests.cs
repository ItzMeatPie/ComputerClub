using NUnit.Framework;
using Moq;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net.Http.Headers;
using TextBox = System.Windows.Forms.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Computer_Club_App.GPUs;
using Computer_Club_App.CPUs;

namespace Computer_Club_App
{
    [TestFixture]
    public class Requests_Add_Tests
    {
        private Mock<DataBase> _mockDb;
        private Requests_Add _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();
            _form = new Requests_Add { db = _mockDb.Object };
        }

        //Проверка на валидность данных
        [Test]
        public void Test_ValidateInput_ValidData()
        {
            // Arrange
            _form.TextBox1.Text = Convert.ToString(1);
            _form.TextBox2.Text = Convert.ToString(1);
            _form.MaskedTextBox2.Text = "20.12.2024";
            _form.TextBox5.Text = "Стандартный";
            _form.TextBox6.Text = Convert.ToString(20);
            _form.ComboBox2.Text = Convert.ToString(1);
            _form.TextBox8.Text = Convert.ToString(2);
            _form.TextBox9.Text = "Иван";
            _form.TextBox10.Text = "Иванов";
            _form.MaskedTextBox1.Text = "+7(987)654-32-21";

            // Act
            bool isValid = _form.ValidateInput();

            Assert.That(isValid, "Валидация должна пройти успешно.");
        }
    }

    [TestFixture]
    public class Requests_Del_Tests
    {
        private Mock<DataBase> _mockDb;
        private Mock<TextBox> _mockTextBox1;
        private Mock<MessageBoxService> _mockMessageBox;
        private Requests_Del _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();

            // Создаем мок для TextBox
            _mockTextBox1 = new Mock<TextBox>();

            // Создаем мок для MessageBox
            _mockMessageBox = new Mock<MessageBoxService>();

            // Создаем экземпляр формы и подменяем зависимости
            _form = new Requests_Del
            {
                db = _mockDb.Object,
                TextBox1 = _mockTextBox1.Object,
                MessageBox = _mockMessageBox.Object
            };
        }

        //Проверка на пустое поле
        [Test]
        public void Test_button1_Click_EmptyInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns(string.Empty);

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Пожалуйста, введите номер заявки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }

        //Инвалидность данных
        [Test]
        public void Test_button1_Click_InvalidInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns("abc");

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Номер заявки должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }
    }


    [TestFixture]
    public class GPUs_Add_Tests
    {
        private Mock<DataBase> _mockDb;
        private GPU_Add _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();
            _form = new GPU_Add { db = _mockDb.Object };
        }

        //Проверка на валидность данных
        [Test]
        public void Test_ValidateInput_ValidData()
        {
            // Arrange
            _form.TextBox1.Text = Convert.ToString(5);
            _form.ComboBox1.Text = Convert.ToString("RTX 3070ti");

            // Act
            bool isValid = _form.ValidateInput();

            Assert.That(isValid, "Валидация должна пройти успешно.");
        }
    }

    [TestFixture]
    public class GPUs_Del_Tests
    {
        private Mock<DataBase> _mockDb;
        private Mock<TextBox> _mockTextBox1;
        private Mock<MessageBoxService> _mockMessageBox;
        private GPU_Delete _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();

            // Создаем мок для TextBox
            _mockTextBox1 = new Mock<TextBox>();

            // Создаем мок для MessageBox
            _mockMessageBox = new Mock<MessageBoxService>();

            // Создаем экземпляр формы и подменяем зависимости
            _form = new GPU_Delete
            {
                db = _mockDb.Object,
                TextBox1 = _mockTextBox1.Object,
                MessageBox = _mockMessageBox.Object
            };
        }

        //Проверка на пустое поле
        [Test]
        public void Test_button1_Click_EmptyInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns(string.Empty);

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Пожалуйста, введите номер видеокарты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }

        //Инвалидность данных
        [Test]
        public void Test_button1_Click_InvalidInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns("abc");

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Номер видеокарты должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }
    }

    [TestFixture]
    public class GСUs_Add_Tests
    {
        private Mock<DataBase> _mockDb;
        private CPU_Add _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();
            _form = new CPU_Add { db = _mockDb.Object };
        }

        //Проверка на валидность данных
        [Test]
        public void Test_ValidateInput_ValidData()
        {
            // Arrange
            _form.TextBox1.Text = Convert.ToString(5);
            _form.ComboBox1.Text = Convert.ToString("RTX 3070ti");

            // Act
            bool isValid = _form.ValidateInput();

            Assert.That(isValid, "Валидация должна пройти успешно.");
        }
    }

    [TestFixture]
    public class СPUs_Del_Tests
    {
        private Mock<DataBase> _mockDb;
        private Mock<TextBox> _mockTextBox1;
        private Mock<MessageBoxService> _mockMessageBox;
        private CPU_Delete _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();

            // Создаем мок для TextBox
            _mockTextBox1 = new Mock<TextBox>();

            // Создаем мок для MessageBox
            _mockMessageBox = new Mock<MessageBoxService>();

            // Создаем экземпляр формы и подменяем зависимости
            _form = new CPU_Delete
            {
                db = _mockDb.Object,
                TextBox1 = _mockTextBox1.Object,
                MessageBox = _mockMessageBox.Object
            };
        }

        //Проверка на пустое поле
        [Test]
        public void Test_button1_Click_EmptyInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns(string.Empty);

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Пожалуйста, введите номер процессора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }

        //Инвалидность данных
        [Test]
        public void Test_button1_Click_InvalidInput()
        {
            // Arrange
            _mockTextBox1.Setup(t => t.Text).Returns("abc");

            // Act
            _form.button1_Click(null, null);

            // Assert
            _mockMessageBox.Verify(m => m.Show("Номер процессора должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning), Times.Once);
        }
    }

    [TestFixture]
    public class Tariffs_Add_Tests
    {
        private Mock<DataBase> _mockDb;
        private Tariffs_Add _form;

        [SetUp]
        public void Setup()
        {
            // Создаем мок для DataBase
            _mockDb = new Mock<DataBase>();
            _form = new Tariffs_Add { db = _mockDb.Object };
        }

        //Проверка на валидность данных
        [Test]
        public void Test_ValidateInput_ValidData()
        {
            // Arrange
            _form.TextBox1.Text = Convert.ToString(4);
            _form.TextBox2.Text = Convert.ToString("Элитный");
            _form.TextBox3.Text = Convert.ToString(1000);

            // Act
            bool isValid = _form.ValidateInput();

            Assert.That(isValid, "Валидация должна пройти успешно.");
        }
    }

    // Вспомогательный класс для мока MessageBox
    public class MessageBoxService
    {
        public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }
    }
}