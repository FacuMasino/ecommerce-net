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

        public List<Category> List()
        {
            List<Category> categories = new List<Category>();

            try
            {
                _dataAccess.SetQuery("select CategoryId, CategoryDescription from Categories");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Category category = new Category();

                    category.Id = (int)_dataAccess.Reader["CategoryId"];

                    category.Description = _dataAccess.Reader["CategoryDescription"]?.ToString();
                    category.Description = category.Description ?? "";

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
                _dataAccess.SetQuery("select CategoryDescription from Categories where CategoryId = @CategoryId");
                _dataAccess.SetParameter("@CategoryId", categoryId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    category.Id = categoryId;

                    category.Description = _dataAccess.Reader["CategoryDescription"]?.ToString();
                    category.Description = category.Description ?? "";
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
                _dataAccess.SetQuery("insert into Categories (CategoryDescription) values (@CategoryDescription)");
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
                    "update Categories set CategoryDescription = @CategoryDescription where CategoryId = @CategoryId"
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
                    $"Ocurrió un error al verificar si la categoría {category?.Description} existe.",
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
                _dataAccess.SetQuery("select CategoryId from Categories where CategoryDescription = @CategoryDescription");
                _dataAccess.SetParameter("@CategoryDescription", category.Description);
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
            if (category.Description != null)
            {
                _dataAccess.SetParameter("@CategoryDescription", category.Description);
            }
        }
    }
}
