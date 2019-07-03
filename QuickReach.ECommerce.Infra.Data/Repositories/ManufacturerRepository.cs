﻿using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ECommerceDbContext context) : base(context)
        {
        }

        public override Manufacturer Retrieve(int entityId)
        {
            var entity = this.context.Manufacturers
                                .Include(m => m.ProductManufacturers)
                                .Where(m => m.ID == entityId)
                                .FirstOrDefault();
            return entity;
        }
        public IEnumerable<Manufacturer> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var results = this.context.Manufacturers
                                .AsNoTracking()
                                .Where(m => m.Name.Contains(search) || m.Description.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return results;
        }
    }
}
