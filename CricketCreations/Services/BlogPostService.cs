using AutoMapper;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Interfaces;
using CricketCreationsRepository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using CricketCreationsRepository.Models;

namespace CricketCreations.Services
{
    public class BlogPostService : IBlogPostService
    {
        private IBlogPostRepository _blogPostRepository;
        private IMapper _mapper;
        public BlogPostService(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public async Task<List<BlogPost>> Read(int page, int count)
        {
            List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.Read(page, count);
            return blogPostDTOs.Select(b => _convertToBlogPost(b)).ToList();
        }

        public async Task<List<BlogPost>> Read(int page, int count, int userId)
        {
            List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.Read(page, count, userId);
            return blogPostDTOs.Select(b => _convertToBlogPost(b)).ToList();
        }

        public async Task<List<BlogPost>> AdminRead(int page, int count, int userId)
        {
            List<BlogPostDTO> blogPostDTOs = await _blogPostRepository.AdminRead(page, count, userId);
            return blogPostDTOs.Select(b => _convertToBlogPost(b)).ToList();
        }

        public async Task<BlogPost> Read(int id)
        {
            BlogPostDTO blogPostDTO = await _blogPostRepository.Read(id);
            return _convertToBlogPost(blogPostDTO);
        }

        public async Task<BlogPost> Create(BlogPost blogPost, int userId)
        {
            BlogPostDTO blogPostDTO = _convertToBlogPostDTO(blogPost);
            BlogPostDTO createdBlogPostDTO = await _blogPostRepository.Create(blogPostDTO, userId);
            return _convertToBlogPost(createdBlogPostDTO);
        }

        public async Task<BlogPost> Update(BlogPost blogPost, int userId)
        {
            BlogPostDTO blogPostDTO = _convertToBlogPostDTO(blogPost);
            BlogPostDTO updatedBlotPostDTO = await _blogPostRepository.Update(blogPostDTO, userId);
            return _convertToBlogPost(updatedBlotPostDTO);
        }

        public async Task<bool> Delete(int id)
        {
            return await _blogPostRepository.Delete(id);
        }
        private BlogPost _convertToBlogPost(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO == null)
            {
                return null;
            }
            return _mapper.Map<BlogPost>(blogPostDTO);
        }
        private BlogPostDTO _convertToBlogPostDTO(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return null;
            }
            return _mapper.Map<BlogPostDTO>(blogPost);
        }

        public async Task<int> GetCount()
        {
            return await _blogPostRepository.GetCount();
        }

        public async Task<int> GetCount(int id)
        {
            return await _blogPostRepository.GetCount(id);
        }
    }
}
