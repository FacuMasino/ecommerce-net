using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class ImagesManager
    {
        private readonly DataAccess _dataAccess = new DataAccess();

        public List<Image> List(int productId)
        {
            List<Image> images = new List<Image>();

            try
            {
                _dataAccess.SetQuery("select * from Images where ProductId = @ProductId");
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Image auxImg = new Image();
                    auxImg.Id = _dataAccess.Reader["ImageId"] as int? ?? auxImg.Id;
                    auxImg.Url = _dataAccess.Reader["ImageUrl"]?.ToString();

                    images.Add(auxImg);
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

            return images;
        }

        public void Add(Image image, int productId)
        {
            try
            {
                _dataAccess.SetQuery(
                    "insert into Images (ProductId, ImageUrl) values (@ProductId, @ImageUrl)"
                );
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.SetParameter("@ImageUrl", image.Url);
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

        public void Add(List<Image> images, int productId)
        {
            try
            {
                foreach (Image image in images)
                {
                    _dataAccess.SetQuery(
                        "insert into Images (ProductId, ImageUrl) values (@ProductId, @ImageUrl)"
                    );
                    _dataAccess.SetParameter("@ProductId", productId);
                    _dataAccess.SetParameter("@ImageUrl", image.Url);
                    _dataAccess.ExecuteAction();
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
        }

        public void Edit(Image image)
        {
            try
            {
                _dataAccess.SetQuery(
                    "update Images set ImageUrl = @ImageUrl where ImageId = @ImageId"
                );
                _dataAccess.SetParameter("@ImageId", image.Id);
                _dataAccess.SetParameter("@ImageUrl", image.Url);
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

        public void Delete(Image image)
        {
            try
            {
                _dataAccess.SetQuery("delete from Images where ImageId = @ImageId");
                _dataAccess.SetParameter("@ImageId", image.Id);
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

        public int GetId(Image image)
        {
            if (image == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select ImageId from Images where ImageUrl = @ImageUrl");
                _dataAccess.SetParameter("@ImageUrl", image.Url);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["ImageId"];
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

        public void CheckDeleted(Product product)
        {
            List<Image> currentList = List(product.Id);
            foreach (Image image in currentList)
            {
                if (product.Images.FindIndex(im => im.Url == image.Url) == -1)
                {
                    Delete(image);
                }
            }
        }
    }
}
