using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalServerSide.Models.DAL;
namespace finalServerSide.Models
{
    public class ClubMember
    {
        int userId;
        int seriesId;
        public ClubMember() { }
        public ClubMember(int userId, int seriesId)
        {
            this.UserId = userId;
            this.SeriesId = seriesId;
        }

        public int UserId { get => userId; set => userId = value; }
        public int SeriesId { get => seriesId; set => seriesId = value; }

        public ClubMember Get(int seriesId, int userId)
        {
            ClubMemderDB cm = new ClubMemderDB();
            return cm.Get(seriesId, userId);
        }
        public int Insert(int seriesId, int userId)
        {
            ClubMemderDB us = new ClubMemderDB();
            return us.Insert(seriesId,userId); //return 1/-1;
        }



    }
}