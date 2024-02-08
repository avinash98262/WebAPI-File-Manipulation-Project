﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FileContext :DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) 
            : base(options) 
        {

        }

        public DbSet<File> Files { get; set; }
    }
}
