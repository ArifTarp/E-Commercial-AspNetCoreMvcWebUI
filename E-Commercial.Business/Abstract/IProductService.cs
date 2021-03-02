using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetProductsByCategoryId(int categoryId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        Product GetProductByProductId(int productId);
    }
}
