using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Business.Abstract;
using E_Commercial.DataAccess.Abstract;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productDal.GetList(p => p.CategoryId == categoryId || categoryId==0);
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Delete(int productId)
        {      
            _productDal.Delete(new Product{ProductId = productId});
        }

        public Product GetProductByProductId(int productId)
        {
            return _productDal.Get(p=>p.ProductId==productId);
        }
    }
}
