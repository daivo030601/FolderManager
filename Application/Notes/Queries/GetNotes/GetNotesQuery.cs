using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.NoteDto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetNotes
{
    public class GetNotesQuery : IRequest<GetNotesResponse> 
    {
        public int FolderId { get; set; }
    }
    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, GetNotesResponse>
    {
        private readonly IFolderManagerDbContext _context;
        private readonly IMapper _mapper;

        public GetNotesQueryHandler(IFolderManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetNotesResponse> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            return new GetNotesResponse
            {
                ResponseData = await _context.Notes
                .Where(n => n.FolderId == request.FolderId)
                .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }
}
