using Core.Domain.Commands;
using Core.Infra.Data.Context;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infra.Data.UoW
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly CoreContext  _context;

        public UnityOfWork(CoreContext context)
        {
            _context = context;
        }

        public async  Task<bool> Commit()
        {
          
            return await _context.SaveChangesAsync()> 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
