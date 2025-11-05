using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg
{
    public class Product : AggregateRoot
    {
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public long SubCategoryId { get; private set; }
        public long? SecondarySubCategoryId { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }

        public Product(string title, string imageName, string description, long categoryId, long subCategoryId, 
            long? secondarySubCategoryId, string slug, SeoData seoData, IProductDomainService domainService)
        {
            Guard(title, imageName, description, slug, domainService);
            Title=title;
            ImageName=imageName;
            Description=description;
            CategoryId=categoryId;
            SubCategoryId=subCategoryId;
            SecondarySubCategoryId=secondarySubCategoryId;
            Slug=slug.ToSlug();
            SeoData=seoData;
        }

        public void Edit(string title, string imageName, string description, long categoryId, long subCategoryId, 
            long? secondarySubCategoryId, string slug, SeoData seoData, IProductDomainService domainService)
        {
            Guard(title, imageName, description, slug, domainService);
            Title =title;
            ImageName=imageName;
            Description=description;
            CategoryId=categoryId;
            SubCategoryId=subCategoryId;
            SecondarySubCategoryId=secondarySubCategoryId;
            Slug=slug.ToSlug();
            SeoData=seoData;
        }

        public void AddImages(ProductImage image)
        {
            image.ProductId=Id;
            Images.Add(image);
        }


        public void RemoveImages(long id)
        {
            var image = Images.FirstOrDefault(x => x.Id == id);
            if (image == null)
                return;
            Images.Remove(image);
        }

        public void AddSpecifications(List<ProductSpecification> specifications)
        {
            foreach (var spec in specifications)
            {
                spec.ProductId=Id;
            }
            Specifications =specifications;
        }

        public void Guard(string title, string imageName, string description, string slug, IProductDomainService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            NullOrEmptyDomainDataException.CheckString(description, nameof(description));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (slug != Slug)
                if (domainService.IsSlugExist(slug))
                    throw new SlugIsDuplicateException("This Slug Is Already Exist");
        } 
    }

}


