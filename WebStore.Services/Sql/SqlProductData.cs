using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    /// <summary>
    /// Layer between CatalogController and Database
    /// Responsible for getting, updating for controller and view data transfer to the database 
    /// </summary>
    public class SqlProductData : IProductData
    {
        private readonly IMapper _mapper;
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            var brands = _mapper.Map<IEnumerable<BrandDto>>(_context.Brands.ToList());

            return brands;
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include("Brand")
                .Include("Section")
                .Where(p => !p.IsDelete)
                .AsQueryable();

            if (filter.BrandId.HasValue)
                query = query.Where(p => p.BrandId.HasValue &&
                                         p.BrandId.Value.Equals(filter.BrandId.Value));

            if (filter.SectionId.HasValue)
                query = query.Where(p => p.SectionId.Equals(filter.SectionId));

            if (filter.Ids != null && filter.Ids.Count > 0)
                query = query.Where(p => filter.Ids.Contains(p.Id));

            var model = new PagedProductDto
            {
                TotalCount = query.Count()
            };

            if (filter.PageSize.HasValue)
            {
                model.Products = _mapper.Map<IEnumerable<ProductDto>>(query
                    .OrderBy(c => c.Order)
                    .Skip((filter.Page - 1) * filter.PageSize.Value)
                    .Take(filter.PageSize.Value));
            }
            else
            {
                model.Products = _mapper.Map<IEnumerable<ProductDto>>(query
                    .OrderBy(c => c.Order));
            }
            
            return model;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            var sections = _mapper.Map<IEnumerable<SectionDto>>(_context.Sections.ToList());

            return sections;
        }

        public int GetBrandProductCount(int id)
            => _context.Products.Count(p => p.BrandId.HasValue && p.BrandId.Value == id);

        public ProductDto GetProductById(int id)
        {
            var product = _mapper.Map<ProductDto>(_context.Products
                .Include("Brand")
                .Include("Section")
                .FirstOrDefault(p => p.Id == id && !p.IsDelete));

            return product;
        }

        public BrandDto GetBrandById(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);

            return _mapper.Map<BrandDto>(brand);
        }

        public SectionDto GetSectionById(int id)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id);

            return _mapper.Map<SectionDto>(section);
        }

        public SaveResult CreateProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);

                _context.Products.Add(product);
                _context.SaveChanges();
                return new SaveResult
                {
                    IsSuccess = true
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new SaveResult
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                return new SaveResult
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        ex.Message
                    }
                };
            }
            catch (Exception e)
            {
                return new SaveResult
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        e.Message
                    }
                };
            }
        }

        public SaveResult UpdateProduct(ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productDto.Id);

            if (product == null)
            {
                return new SaveResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Entity not exist" }
                };
            }

            product.BrandId = productDto.Brand.Id;
            product.SectionId = productDto.Section.Id;
            product.ImageUrl = productDto.ImageUrl;
            product.Order = productDto.Order;
            product.Price = productDto.Price;
            product.Name = productDto.Name;

            try
            {
                _context.SaveChanges();
                return new SaveResult
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new SaveResult
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        e.Message
                    }
                };
            }

        }

        public SaveResult DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return new SaveResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Entity not exist" }
                };
            }
            try
            {
                product.IsDelete = true;
                _context.SaveChanges();
                return new SaveResult()
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new SaveResult
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        e.Message
                    }
                };
            }
        }
    }
}
