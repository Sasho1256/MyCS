﻿using System.Threading.Tasks;

namespace Services
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public interface ISeedService
    {
        public Task<ICollection<ExceptionModel>> SeedRecords(IFormFile file);
    }
}
