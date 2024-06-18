using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CategoriesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public List<Category> List(int productId = 0)
        {
            List<Category> categories = new List<Category>();
            string query = "select C.CategoryId, C.CategoryName from Categories C";

            if (0 < productId)
            {
                query +=
                    " inner join ProductCategories PC on C.CategoryId = PC.CategoryId where PC.ProductId = @ProductId";
                _dataAccess.SetParameter("@ProductId", productId);
            }

            try
            {
                _dataAccess.SetQuery(query);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Category category = new Category();

                    category.Id = (int)_dataAccess.Reader["CategoryId"];

                    category.Name = _dataAccess.Reader["CategoryName"]?.ToString();
                    category.Name = category.Name ?? "";

                    categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return categories;
        }

        public Category Read(int categoryId)
        {
            Category category = new Category();

            try
            {
                _dataAccess.SetQuery(
                    "select CategoryName from Categories where CategoryId = @CategoryId"
                );
                _dataAccess.SetParameter("@CategoryId", categoryId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    category.Id = categoryId;

                    category.Name = _dataAccess.Reader["CategoryName"]?.ToString();
                    category.Name = category.Name ?? "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return category;
        }

        public void Add(Category category)
        {
            try
            {
                _dataAccess.SetQuery(
                    "insert into Categories (CategoryName) values (@CategoryName)"
                );
                SetParameters(category);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public void Edit(Category category)
        {
            try
            {
                _dataAccess.SetQuery(
                    "update Categories set CategoryName = @CategoryName where CategoryId = @CategoryId"
                );
                _dataAccess.SetParameter("@CategoryId", category.Id);
                SetParameters(category);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public void Delete(Category category)
        {
            try
            {
                _dataAccess.SetQuery("delete from Categories where CategoryId = @CategoryId");
                _dataAccess.SetParameter("@CategoryId", category.Id);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Verifica si la categoria del artículo no pertenece a ningun otro y en tal caso la elimina.
        /// </summary>
        public void PurgeCategory(Category category)
        {
            bool categoryInUse = CategoryIsInUse(category);
            Debug.Print($"Verificando si la categoría {category} está en uso => {categoryInUse}");

            if (!categoryInUse)
            {
                Delete(category);
            }
        }

        public void AddRelation(Category category, int productId)
        {
            try
            {
                _dataAccess.SetQuery(
                    "insert into ProductCategories (ProductId, CategoryId) values (@ProductId, @CategoryId)"
                );
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.SetParameter("@CategoryId", category.Id);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private bool CategoryIsInUse(Category category)
        {
            try
            {
                _dataAccess.SetQuery(
                    "select count(*) as Total from Products where CategoryId = @CategoryId"
                );
                _dataAccess.SetParameter("@CategoryId", category.Id);
                return _dataAccess.ExecuteScalar() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Ocurrió un error al verificar si la categoría {category?.Name} existe.",
                    ex
                );
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public int GetId(Category category)
        {
            if (category == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery(
                    "select CategoryId from Categories where CategoryName = @CategoryName"
                );
                _dataAccess.SetParameter("@CategoryName", category.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["CategoryId"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return id;
        }

        private void SetParameters(Category category)
        {
            if (category.Name != null)
            {
                _dataAccess.SetParameter("@CategoryName", category.Name);
            }
        }

        private List<int> GetCategoriesIds(int productId)
        {
            List<int> categoriesId = new List<int>();

            return categoriesId;
        }
    }
}
