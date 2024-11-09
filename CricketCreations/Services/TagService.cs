using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<Tag> Create(Tag tag, int blogPostId, int userId)
        {
            TagDTO tagDTO = _convertToTagDTO(tag);
            TagDTO updatedTagDTO = await _tagRepository.Create(tagDTO, blogPostId, userId);
            return _convertToTag(updatedTagDTO);
        }

        public async Task<bool> Delete(int id)
        {
            return await _tagRepository.Delete(id);
        }

        public async Task<Tag> Read(int id)
        {
            TagDTO tagDTO = await _tagRepository.Read(id);
            return _convertToTag(tagDTO);
        }

        public async Task<List<Tag>> Read(int page, int count)
        {
            List<TagDTO> tagDTOs = await _tagRepository.Read(page, count);
            return tagDTOs.Select(t => _convertToTag(t)).ToList();
        }

        public async Task<List<Tag>> Read(int page, int count, int userId)
        {
            List<TagDTO> tagDTOs = await _tagRepository.Read(page, count, userId);
            return tagDTOs.Select(t => _convertToTag(t)).ToList();
        }

        public async Task<Tag> Update(Tag tag, int userId)
        {
            TagDTO tagDTO = _convertToTagDTO(tag);
            TagDTO updatedTagDTO = await _tagRepository.Update(tagDTO, userId);
            return _convertToTag(updatedTagDTO);
        }

        public Task<Tag> Create(Tag t, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount()
        {
            return await _tagRepository.GetCount();
        }

        public async Task<int> GetCount(int id)
        {
            return await _tagRepository.GetCount(id);
        }

        public async Task<int> GetCountOfBlogPosts(string tagName)
        {
            return await _tagRepository.GetCountOfBlogPosts(tagName);
        }

        private TagDTO _convertToTagDTO(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }

            return _mapper.Map<Tag, TagDTO>(tag);
        }

        private Tag _convertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }

            return _mapper.Map<TagDTO, Tag>(tagDTO);
        }
    }
}
