using CategoryManager.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CategoryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyStockDbContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new MyStockDbContext();
            LoadCategories();
        }
        private void LoadCategories()
        {
            // Load the data into the DataGrid
            dgvCategories.ItemsSource = _context.Categories.ToList();
        }

        private void dgvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvCategories.SelectedItem is Category selectedCategory)
            {
                txtCategoryID.Text = selectedCategory.CategoryID.ToString();
                txtCategoryName.Text = selectedCategory.CategoryName;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category
            {
                CategoryName = txtCategoryName.Text
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            LoadCategories();
            ClearFields();

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
                if (category != null)
                {
                    category.CategoryName = txtCategoryName.Text;
                    _context.SaveChanges();
                    LoadCategories();
                    ClearFields();
                }
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    LoadCategories();
                    ClearFields();
                }
            }
        }

        private void ClearFields()
        {
            txtCategoryID.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}