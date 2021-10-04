﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApplicationInterface;

namespace TestApplicationDB
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbFactory _dbFactory;

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<int> CommitAsync()
        {
            return _dbFactory.DbContext.SaveChangesAsync();
        }
    }
}
