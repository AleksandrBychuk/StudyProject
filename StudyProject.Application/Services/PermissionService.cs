﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.Application.Services
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionDTO> GetByIdAsync(Guid id)
        {
            var permission = await _context.Permissions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (permission == null) return null;

            return permission.Adapt<PermissionDTO>();
        }

        public async Task<List<PermissionDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var permission = await _context.Permissions.AsNoTracking().Skip(skip).Take(count).ToListAsync();

            return permission.Adapt<List<PermissionDTO>>();
        }

        public async Task<PermissionDTO> Create(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();

            return permission.Adapt<PermissionDTO>();
        }

        public async Task<PermissionDTO> UpdateAsync(Permission permission)
        {
            var permissionToUpdate = await _context.Permissions.FindAsync(permission.Id);

            if (permissionToUpdate == null) return null;

            permissionToUpdate.Name = permission.Name;

            _context.Permissions.Update(permissionToUpdate);
            await _context.SaveChangesAsync();

            return permissionToUpdate.Adapt<PermissionDTO>();
        }

        public async Task<PermissionDTO> DeleteAsync(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null) return null;

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return permission.Adapt<PermissionDTO>();
        }
    }
}