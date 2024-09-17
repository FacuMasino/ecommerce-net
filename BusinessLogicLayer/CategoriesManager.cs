using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CategoriesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public List<Category> List(bool onlyActive = true, int productId = 0)
        {
            List<Category> categories = new List<Category>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Categories");
                _dataAccess.SetParameter("@OnlyActive", onlyActive);
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Category category = new Category();

                    category.Id = (int)_dataAccess.Reader["CategoryId"];
                    category.IsActive = (bool)_dataAccess.Reader["IsActive"];
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
                    "select IsActive, CategoryName from Categories where CategoryId = @CategoryId"
                );
                _dataAccess.SetParameter("@CategoryId", categoryId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    category.Id = categoryId;
                    category.IsActive = (bool)_dataAccess.Reader["IsActive"];
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
                _dataAccess.SetProcedure("SP_Edit_Category");
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

        public void DeleteLogically(Category category)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Delete_Category_Logically");
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

        public void Delete(Category category, bool isLogicalDeletion = true)
        {
            if (isLogicalDeletion == true)
            {
                DeleteLogically(category);
                return;
            }

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

        /// <summary>
        /// Actualiza las relaciones entre un producto y sus categorías.
        /// </summary>
        /// <param name="product">Producto con la nueva lista de categorías</param>
        public void UpdateRelations(Product product)
        {
            List<Category> currentCategories = List(true, product.Id); // Lista actual en la db
            List<Category> newCategories = product.Categories; // Lista nueva en el producto

            // Se queda con la diferencia entre las actuales - las nuevas (Dif. de conjuntos)
            List<Category> categoriesToRemove = currentCategories.Except(newCategories).ToList();

            foreach (Category cat in categoriesToRemove)
            {
                DeleteRelation(cat, product.Id); // Se elimina de la tabla
                currentCategories.Remove(cat); // Se elimina de la lista actual
            }

            // Si ambas tienen la misma cantidad, no hay nuevas, salir
            if (currentCategories.Count == newCategories.Count)
                return;

            // Se queda con la diferencia de las nuevas - las actuales
            List<Category> categoriesToAdd = newCategories.Except(currentCategories).ToList();

            foreach (Category cat in categoriesToAdd)
            {
                AddRelation(cat, product.Id); // Se agrega la nueva a la tabla
            }
        }

        public void DeleteRelation(Category category, int productId)
        {
            try
            {
                _dataAccess.SetQuery(
                    "delete from ProductCategories where CategoryId = @CategoryId and ProductId = @ProductId"
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

        public int CountCategoryRelations(Category category)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Category_Relations");
                _dataAccess.SetParameter("@CategoryId", category.Id);
                return _dataAccess.ExecuteScalar();
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
            _dataAccess.SetParameter("@IsActive", category.IsActive);
            _dataAccess.SetParameter("@CategoryName", category.Name);
        }
    }
}
