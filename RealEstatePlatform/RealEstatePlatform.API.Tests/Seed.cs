using Microsoft.EntityFrameworkCore;
using RealEstatePlatform.Application.Contracts.Interfaces;
using RealEstatePlatform.Domain.Entities;
using RealEstatePlatform.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstatePlatform.API.Tests
{
    public static class Seed
    {
        public static async Task InitializeRealEstatePlatformDbForTests(RealEstatePlatformContext context)
        {
            var properties = new List<Property>
            {
                // Create sample properties
            };

            var propertyTypes = new List<PropertyType>
            {
                // Create sample property types
            };

            var images = new List<Image>
            {
                // Create sample images
            };

            var messages = new List<Message>
            {
                // Create sample messages
            };

            var listingTypes = new List<ListingType>
            {
                ListingType.Create("Test1").Value,
                ListingType.Create("Test2").Value
            };

            context.Properties.AddRange(properties);
            context.PropertyTypes.AddRange(propertyTypes);
            context.Images.AddRange(images);
            context.Messages.AddRange(messages);
            context.ListingTypes.AddRange(listingTypes);

            await context.SaveChangesAsync();

           
        }
    }
}
