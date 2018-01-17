﻿using System;
using System.Collections.Generic;
using UnleashedApp.Models;
using UnleashedApp.Services;
using UnleashedApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static UnleashedApp.Models.DrawableRoom;

namespace UnleashedApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorplanView : ContentPage
    {
        private static List<Space> spaces;
        public FloorplanView()
        {
            InitializeComponent();
            GridService.CreateRooms();
            CreateSpaces();
            //CreateLegendGrid();
            CreateFloorplanGrid();
        }

        /// <summary>
        /// Makes the FloorplanGrid a square
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            double amountOfColumns = FloorplanGrid.ColumnDefinitions.Count;
            double amountOfRows = FloorplanGrid.RowDefinitions.Count;
            if (amountOfColumns > 0 && amountOfRows > 0)
            {
                FloorplanGrid.HeightRequest = width / amountOfColumns * amountOfRows;
            }
        }

        private void CreateSpaces()
        {
            spaces = new List<Space>
            {
                new Space(0,0,0,2),
                new Space(1,0,1,2),
                new Space(2,0,1,2),
                new Space(3,0,0,2),
                new Space(4,0,0,2),
                new Space(5,0,1,2),
                new Space(6,0,1,2),
                new Space(7,0,0,2),
                new Space(8,0,0,2),
                new Space(9,0,1,2),
                new Space(10,0,1,2),
                new Space(11,0,0,2),
                new Space(12,0,0,2),
                new Space(13,0,1,2),
                new Space(14,0,1,2),
                new Space(15,0,0,2),
                new Space(16,0,0,3),
                new Space(17,0,0,3),
                new Space(18,0,0,3),
                new Space(19,0,0,3),
                new Space(20,0,0,3),
                new Space(21,0,0,3),
                new Space(22,0,0,3),
                new Space(23,0,0,3),
                new Space(24,0,0,4),
                new Space(25,0,0,4),
                new Space(26,0,0,4),
                new Space(27,0,0,4),
                new Space(28,0,0,4),
                new Space(29,0,0,4),
                new Space(30,0,0,4),
                new Space(31,0,0,4),
                new Space(32,0,0,5),
                new Space(33,0,0,5),
                new Space(34,0,0,5),
                new Space(35,0,0,5),
                new Space(36,0,0,5),
                new Space(37,0,0,6),
                new Space(38,0,0,6),
                new Space(39,0,0,6),
                new Space(40,0,0,6),
                new Space(41,0,0,6),
                new Space(42,0,0,6),
                new Space(43,0,0,6),
                new Space(44,0,0,7),
                new Space(45,0,0,7),
                new Space(46,0,0,7),
                new Space(47,0,0,7),
                new Space(48,0,0,7),
                new Space(49,0,0,7),
                new Space(50,0,0,7),

                new Space(0,1,0,2),
                new Space(1,1,1,2),
                new Space(2,1,1,2),
                new Space(3,1,0,2),
                new Space(4,1,0,2),
                new Space(5,1,1,2),
                new Space(6,1,1,2),
                new Space(7,1,0,2),
                new Space(8,1,0,2),
                new Space(9,1,1,2),
                new Space(10,1,1,2),
                new Space(11,1,0,2),
                new Space(12,1,0,2),
                new Space(13,1,1,2),
                new Space(14,1,1,2),
                new Space(15,1,0,2),
                new Space(16,1,0,3),
                new Space(17,1,0,3),
                new Space(18,1,0,3),
                new Space(19,1,0,3),
                new Space(20,1,0,3),
                new Space(21,1,0,3),
                new Space(22,1,0,3),
                new Space(23,1,0,3),
                new Space(24,1,0,4),
                new Space(25,1,0,4),
                new Space(26,1,1,4),
                new Space(27,1,1,4),
                new Space(28,1,1,4),
                new Space(29,1,1,4),
                new Space(30,1,1,4),
                new Space(31,1,0,4),
                new Space(32,1,0,5),
                new Space(33,1,1,5),
                new Space(34,1,1,5),
                new Space(35,1,1,5),
                new Space(36,1,0,5),
                new Space(37,1,0,6),
                new Space(38,1,1,6),
                new Space(39,1,1,6),
                new Space(40,1,1,6),
                new Space(41,1,1,6),
                new Space(42,1,1,6),
                new Space(43,1,0,6),
                new Space(44,1,0,7),
                new Space(45,1,0,7),
                new Space(46,1,0,7),
                new Space(47,1,0,7),
                new Space(48,1,0,7),
                new Space(49,1,0,7),
                new Space(50,1,0,7),

                new Space(0,2,0,2),
                new Space(1,2,1,2),
                new Space(2,2,1,2),
                new Space(3,2,0,2),
                new Space(4,2,0,2),
                new Space(5,2,1,2),
                new Space(6,2,1,2),
                new Space(7,2,0,2),
                new Space(8,2,0,2),
                new Space(9,2,1,2),
                new Space(10,2,1,2),
                new Space(11,2,0,2),
                new Space(12,2,0,2),
                new Space(13,2,1,2),
                new Space(14,2,1,2),
                new Space(15,2,0,2),
                new Space(16,2,0,3),
                new Space(17,2,0,3),
                new Space(18,2,0,3),
                new Space(19,2,0,3),
                new Space(20,2,0,3),
                new Space(21,2,0,3),
                new Space(22,2,0,3),
                new Space(23,2,0,3),
                new Space(24,2,0,4),
                new Space(25,2,0,4),
                new Space(26,2,0,4),
                new Space(27,2,0,4),
                new Space(28,2,0,4),
                new Space(29,2,0,4),
                new Space(30,2,0,4),
                new Space(31,2,0,4),
                new Space(32,2,0,5),
                new Space(33,2,0,5),
                new Space(34,2,0,5),
                new Space(35,2,0,5),
                new Space(36,2,0,5),
                new Space(37,2,0,6),
                new Space(38,2,0,6),
                new Space(39,2,0,6),
                new Space(40,2,0,6),
                new Space(41,2,0,6),
                new Space(42,2,0,6),
                new Space(43,2,0,6),
                new Space(44,2,0,7),
                new Space(45,2,0,7),
                new Space(46,2,0,7),
                new Space(47,2,0,7),
                new Space(48,2,0,7),
                new Space(49,2,0,7),
                new Space(50,2,0,7),

                new Space(0,3,0,1),
                new Space(1,3,0,1),
                new Space(2,3,0,1),
                new Space(3,3,0,1),
                new Space(4,3,0,1),
                new Space(5,3,0,1),
                new Space(6,3,0,1),
                new Space(7,3,0,1),
                new Space(8,3,0,1),
                new Space(9,3,0,1),
                new Space(10,3,0,1),
                new Space(11,3,0,1),
                new Space(12,3,0,1),
                new Space(13,3,0,1),
                new Space(14,3,0,1),
                new Space(15,3,0,1),
                new Space(16,3,0,1),
                new Space(17,3,0,1),
                new Space(18,3,0,1),
                new Space(19,3,0,1),
                new Space(20,3,0,1),
                new Space(21,3,0,1),
                new Space(22,3,0,1),
                new Space(23,3,0,1),
                new Space(24,3,0,1),
                new Space(25,3,0,1),
                new Space(26,3,0,1),
                new Space(27,3,0,1),
                new Space(28,3,0,1),
                new Space(29,3,0,1),
                new Space(30,3,0,1),
                new Space(31,3,0,1),
                new Space(32,3,0,1),
                new Space(33,3,0,1),
                new Space(34,3,0,1),
                new Space(35,3,0,1),
                new Space(36,3,0,1),
                new Space(37,3,0,1),
                new Space(38,3,0,1),
                new Space(39,3,0,1),
                new Space(40,3,0,1),
                new Space(41,3,0,1),
                new Space(42,3,0,7),
                new Space(43,3,0,7),
                new Space(44,3,0,7),
                new Space(45,3,0,7),
                new Space(46,3,0,7),
                new Space(47,3,0,7),
                new Space(48,3,0,7),
                new Space(49,3,0,7),
                new Space(50,3,0,7),

                new Space(0,4,0,1),
                new Space(1,4,0,1),
                new Space(2,4,0,1),
                new Space(3,4,0,1),
                new Space(4,4,0,1),
                new Space(5,4,0,1),
                new Space(6,4,0,1),
                new Space(7,4,0,1),
                new Space(8,4,0,1),
                new Space(9,4,0,1),
                new Space(10,4,0,1),
                new Space(11,4,0,1),
                new Space(12,4,0,1),
                new Space(13,4,0,1),
                new Space(14,4,0,1),
                new Space(15,4,0,1),
                new Space(16,4,0,1),
                new Space(17,4,0,1),
                new Space(18,4,0,1),
                new Space(19,4,0,1),
                new Space(20,4,0,1),
                new Space(21,4,0,1),
                new Space(22,4,0,1),
                new Space(23,4,0,1),
                new Space(24,4,0,1),
                new Space(25,4,0,1),
                new Space(26,4,0,1),
                new Space(27,4,0,1),
                new Space(28,4,0,1),
                new Space(29,4,0,1),
                new Space(30,4,0,1),
                new Space(31,4,0,1),
                new Space(32,4,0,1),
                new Space(33,4,0,1),
                new Space(34,4,0,1),
                new Space(35,4,0,1),
                new Space(36,4,0,1),
                new Space(37,4,0,1),
                new Space(38,4,0,1),
                new Space(39,4,0,1),
                new Space(40,4,0,1),
                new Space(41,4,0,1),
                new Space(42,4,0,7),
                new Space(43,4,0,7),
                new Space(44,4,0,7),
                new Space(45,4,0,7),
                new Space(46,4,0,7),
                new Space(47,4,0,7),
                new Space(48,4,0,7),
                new Space(49,4,0,7),
                new Space(50,4,0,7),

                new Space(0,5,0,8),
                new Space(1,5,1,8),
                new Space(2,5,1,8),
                new Space(3,5,0,8),
                new Space(4,5,0,9),
                new Space(5,5,1,9),
                new Space(6,5,1,9),
                new Space(7,5,0,9),
                new Space(8,5,0,1),
                new Space(9,5,0,1),
                new Space(10,5,0,10),
                new Space(11,5,0,10),
                new Space(12,5,0,10),
                new Space(13,5,0,10),
                new Space(14,5,0,10),
                new Space(15,5,0,11),
                new Space(16,5,1,11),
                new Space(17,5,1,11),
                new Space(18,5,0,11),
                new Space(19,5,0,1),
                new Space(20,5,0,12),
                new Space(21,5,1,12),
                new Space(22,5,1,12),
                new Space(23,5,0,12),
                new Space(24,5,0,12),
                new Space(25,5,1,12),
                new Space(26,5,1,12),
                new Space(27,5,0,12),
                new Space(28,5,0,12),
                new Space(29,5,1,12),
                new Space(30,5,1,12),
                new Space(31,5,0,12),
                new Space(32,5,0,12),
                new Space(33,5,1,12),
                new Space(34,5,1,12),
                new Space(35,5,0,12),
                new Space(36,5,0,13),
                new Space(37,5,0,13),
                new Space(38,5,0,13),
                new Space(39,5,0,13),
                new Space(40,5,0,14),
                new Space(41,5,0,14),
                new Space(42,5,0,7),
                new Space(43,5,0,7),
                new Space(44,5,0,7),
                new Space(45,5,0,7),
                new Space(46,5,0,7),
                new Space(47,5,0,7),
                new Space(48,5,0,7),
                new Space(49,5,0,7),
                new Space(50,5,0,7),

                new Space(0,6,0,8),
                new Space(1,6,1,8),
                new Space(2,6,1,8),
                new Space(3,6,0,8),
                new Space(4,6,0,9),
                new Space(5,6,1,9),
                new Space(6,6,1,9),
                new Space(7,6,0,9),
                new Space(8,6,0,1),
                new Space(9,6,0,1),
                new Space(10,6,0,10),
                new Space(11,6,0,10),
                new Space(12,6,0,10),
                new Space(13,6,0,10),
                new Space(14,6,0,10),
                new Space(15,6,0,11),
                new Space(16,6,1,11),
                new Space(17,6,1,11),
                new Space(18,6,0,11),
                new Space(19,6,0,1),
                new Space(20,6,0,12),
                new Space(21,6,1,12),
                new Space(22,6,1,12),
                new Space(23,6,0,12),
                new Space(24,6,0,12),
                new Space(25,6,1,12),
                new Space(26,6,1,12),
                new Space(27,6,0,12),
                new Space(28,6,0,12),
                new Space(29,6,1,12),
                new Space(30,6,1,12),
                new Space(31,6,0,12),
                new Space(32,6,0,12),
                new Space(33,6,1,12),
                new Space(34,6,1,12),
                new Space(35,6,0,12),
                new Space(36,6,0,13),
                new Space(37,6,0,13),
                new Space(38,6,0,13),
                new Space(39,6,0,13),
                new Space(40,6,0,14),
                new Space(41,6,0,14),
                new Space(42,6,0,7),
                new Space(43,6,0,7),
                new Space(44,6,0,7),
                new Space(45,6,0,7),
                new Space(46,6,0,7),
                new Space(47,6,0,7),
                new Space(48,6,0,7),
                new Space(49,6,0,7),
                new Space(50,6,0,7),

                new Space(0,7,0,8),
                new Space(1,7,1,8),
                new Space(2,7,1,8),
                new Space(3,7,0,8),
                new Space(4,7,0,1),
                new Space(5,7,0,1),
                new Space(6,7,0,1),
                new Space(7,7,0,1),
                new Space(8,7,0,1),
                new Space(9,7,0,1),
                new Space(10,7,0,10),
                new Space(11,7,0,10),
                new Space(12,7,0,10),
                new Space(13,7,0,10),
                new Space(14,7,0,10),
                new Space(15,7,0,11),
                new Space(16,7,1,11),
                new Space(17,7,1,11),
                new Space(18,7,0,11),
                new Space(19,7,0,1),
                new Space(20,7,0,1),
                new Space(21,7,0,1),
                new Space(22,7,0,1),
                new Space(23,7,0,1),
                new Space(24,7,0,12),
                new Space(25,7,1,12),
                new Space(26,7,1,12),
                new Space(27,7,0,12),
                new Space(28,7,0,1),
                new Space(29,7,0,1),
                new Space(30,7,0,1),
                new Space(31,7,0,1),
                new Space(32,7,0,12),
                new Space(33,7,1,12),
                new Space(34,7,1,12),
                new Space(35,7,0,12),
                new Space(36,7,0,13),
                new Space(37,7,0,13),
                new Space(38,7,0,13),
                new Space(39,7,0,13),
                new Space(40,7,0,14),
                new Space(41,7,0,14),
                new Space(42,7,0,7),
                new Space(43,7,0,7),
                new Space(44,7,0,7),
                new Space(45,7,0,7),
                new Space(46,7,0,7),
                new Space(47,7,0,7),
                new Space(48,7,0,7),
                new Space(49,7,0,7),
                new Space(50,7,0,7),

                new Space(0,8,0,1),
                new Space(1,8,0,1),
                new Space(2,8,0,1),
                new Space(3,8,0,1),
                new Space(4,8,0,1),
                new Space(5,8,0,1),
                new Space(6,8,0,1),
                new Space(7,8,0,1),
                new Space(8,8,0,1),
                new Space(9,8,0,1),
                new Space(10,8,0,1),
                new Space(11,8,0,1),
                new Space(12,8,0,1),
                new Space(13,8,0,1),
                new Space(14,8,0,1),
                new Space(15,8,0,1),
                new Space(16,8,0,1),
                new Space(17,8,0,1),
                new Space(18,8,0,1),
                new Space(19,8,0,1),
                new Space(20,8,0,1),
                new Space(21,8,0,1),
                new Space(22,8,0,1),
                new Space(23,8,0,1),
                new Space(24,8,0,1),
                new Space(25,8,0,1),
                new Space(26,8,0,1),
                new Space(27,8,0,1),
                new Space(28,8,0,1),
                new Space(29,8,0,1),
                new Space(30,8,0,1),
                new Space(31,8,0,1),
                new Space(32,8,0,1),
                new Space(33,8,0,1),
                new Space(34,8,0,1),
                new Space(35,8,0,1),
                new Space(36,8,0,1),
                new Space(37,8,0,1),
                new Space(38,8,0,1),
                new Space(39,8,0,1),
                new Space(40,8,0,1),
                new Space(41,8,0,1),
                new Space(42,8,0,7),
                new Space(43,8,0,7),
                new Space(44,8,0,7),
                new Space(45,8,0,7),
                new Space(46,8,0,7),
                new Space(47,8,0,7),
                new Space(48,8,0,7),
                new Space(49,8,0,7),
                new Space(50,8,0,7),

                new Space(0,9,0,1),
                new Space(1,9,0,1),
                new Space(2,9,0,1),
                new Space(3,9,0,1),
                new Space(4,9,0,1),
                new Space(5,9,0,1),
                new Space(6,9,0,1),
                new Space(7,9,0,1),
                new Space(8,9,0,1),
                new Space(9,9,0,1),
                new Space(10,9,0,1),
                new Space(11,9,0,1),
                new Space(12,9,0,1),
                new Space(13,9,0,1),
                new Space(14,9,0,1),
                new Space(15,9,0,1),
                new Space(16,9,0,1),
                new Space(17,9,0,1),
                new Space(18,9,0,1),
                new Space(19,9,0,1),
                new Space(20,9,0,1),
                new Space(21,9,0,1),
                new Space(22,9,0,1),
                new Space(23,9,0,1),
                new Space(24,9,0,1),
                new Space(25,9,0,1),
                new Space(26,9,0,1),
                new Space(27,9,0,1),
                new Space(28,9,0,1),
                new Space(29,9,0,1),
                new Space(30,9,0,1),
                new Space(31,9,0,1),
                new Space(32,9,0,1),
                new Space(33,9,0,1),
                new Space(34,9,0,1),
                new Space(35,9,0,1),
                new Space(36,9,0,1),
                new Space(37,9,0,1),
                new Space(38,9,0,1),
                new Space(39,9,0,1),
                new Space(40,9,0,1),
                new Space(41,9,0,1),
                new Space(42,9,0,7),
                new Space(43,9,0,7),
                new Space(44,9,0,7),
                new Space(45,9,0,7),
                new Space(46,9,0,7),
                new Space(47,9,0,7),
                new Space(48,9,0,7),
                new Space(49,9,0,7),
                new Space(50,9,0,7),

                new Space(0,10,0,15),
                new Space(1,10,1,15),
                new Space(2,10,1,15),
                new Space(3,10,0,15),
                new Space(4,10,0,15),
                new Space(5,10,1,15),
                new Space(6,10,1,15),
                new Space(7,10,0,15),
                new Space(8,10,0,16),
                new Space(9,10,0,16),
                new Space(10,10,0,16),
                new Space(11,10,0,16),
                new Space(12,10,0,16),
                new Space(13,10,0,16),
                new Space(14,10,0,16),
                new Space(15,10,0,17),
                new Space(16,10,1,17),
                new Space(17,10,1,17),
                new Space(18,10,0,17),
                new Space(19,10,0,18),
                new Space(20,10,1,18),
                new Space(21,10,1,18),
                new Space(22,10,0,18),
                new Space(23,10,0,19),
                new Space(24,10,0,19),
                new Space(25,10,0,19),
                new Space(26,10,0,19),
                new Space(27,10,0,19),
                new Space(28,10,0,19),
                new Space(29,10,0,19),
                new Space(30,10,0,19),
                new Space(31,10,0,20),
                new Space(32,10,0,20),
                new Space(33,10,0,21),
                new Space(34,10,0,21),
                new Space(35,10,0,21),
                new Space(36,10,0,21),
                new Space(37,10,0,22),
                new Space(38,10,0,22),
                new Space(39,10,0,23),
                new Space(40,10,1,23),
                new Space(41,10,1,23),
                new Space(42,10,1,23),
                new Space(43,10,1,23),
                new Space(44,10,1,23),
                new Space(45,10,0,23),
                new Space(46,10,1,23),
                new Space(47,10,1,23),
                new Space(48,10,1,23),
                new Space(49,10,1,23),
                new Space(50,10,1,23),

                new Space(0,11,0,15),
                new Space(1,11,1,15),
                new Space(2,11,1,15),
                new Space(3,11,0,15),
                new Space(4,11,0,15),
                new Space(5,11,1,15),
                new Space(6,11,1,15),
                new Space(7,11,0,15),
                new Space(8,11,0,16),
                new Space(9,11,0,16),
                new Space(10,11,0,16),
                new Space(11,11,0,16),
                new Space(12,11,0,16),
                new Space(13,11,0,16),
                new Space(14,11,0,16),
                new Space(15,11,0,17),
                new Space(16,11,1,17),
                new Space(17,11,1,17),
                new Space(18,11,0,17),
                new Space(19,11,0,18),
                new Space(20,11,1,18),
                new Space(21,11,1,18),
                new Space(22,11,0,18),
                new Space(23,11,0,19),
                new Space(24,11,0,19),
                new Space(25,11,0,19),
                new Space(26,11,0,19),
                new Space(27,11,0,19),
                new Space(28,11,0,19),
                new Space(29,11,0,19),
                new Space(30,11,0,19),
                new Space(31,11,0,20),
                new Space(32,11,0,20),
                new Space(33,11,0,21),
                new Space(34,11,0,21),
                new Space(35,11,0,21),
                new Space(36,11,0,21),
                new Space(37,11,0,22),
                new Space(38,11,0,22),
                new Space(39,11,0,23),
                new Space(40,11,0,23),
                new Space(41,11,0,23),
                new Space(42,11,0,23),
                new Space(43,11,0,23),
                new Space(44,11,0,23),
                new Space(45,11,0,23),
                new Space(46,11,0,23),
                new Space(47,11,0,23),
                new Space(48,11,0,23),
                new Space(49,11,0,23),
                new Space(50,11,0,23),

                new Space(0,12,0,15),
                new Space(1,12,1,15),
                new Space(2,12,1,15),
                new Space(3,12,0,15),
                new Space(4,12,0,15),
                new Space(5,12,1,15),
                new Space(6,12,1,15),
                new Space(7,12,0,15),
                new Space(8,12,0,16),
                new Space(9,12,0,16),
                new Space(10,12,0,16),
                new Space(11,12,0,16),
                new Space(12,12,0,16),
                new Space(13,12,0,16),
                new Space(14,12,0,16),
                new Space(15,12,0,17),
                new Space(16,12,1,17),
                new Space(17,12,1,17),
                new Space(18,12,0,17),
                new Space(19,12,0,18),
                new Space(20,12,1,18),
                new Space(21,12,1,18),
                new Space(22,12,0,18),
                new Space(23,12,0,19),
                new Space(24,12,0,19),
                new Space(25,12,0,19),
                new Space(26,12,0,19),
                new Space(27,12,0,19),
                new Space(28,12,0,19),
                new Space(29,12,0,19),
                new Space(30,12,0,19),
                new Space(31,12,0,20),
                new Space(32,12,0,20),
                new Space(33,12,0,21),
                new Space(34,12,0,21),
                new Space(35,12,0,21),
                new Space(36,12,0,21),
                new Space(37,12,0,22),
                new Space(38,12,0,22),
                new Space(39,12,0,23),
                new Space(40,12,0,23),
                new Space(41,12,0,23),
                new Space(42,12,0,23),
                new Space(43,12,0,23),
                new Space(44,12,0,23),
                new Space(45,12,0,23),
                new Space(46,12,0,23),
                new Space(47,12,0,23),
                new Space(48,12,0,23),
                new Space(49,12,0,23),
                new Space(50,12,0,23),
            };
        }

        #region LegendGrid
        private void CreateLegendGrid()
        {
            GridService.CreateLegendGridColumnDefinitions(LegendGrid);
            GridService.CreateLegendGridRowDefinitions(LegendGrid, GridService.Rooms.Count);
            FillLegendGrid();
        }

        private void FillLegendGrid()
        {
            int i = 1;
            foreach (DrawableRoom room in GridService.Rooms)
            {
                GridService.AddColorLabel(LegendGrid, i, 1, room.Color);
                GridService.AddTextLabel(LegendGrid, i, 2, room.Name);
                i++;
            }
        }
        #endregion

        #region FloorplanGrid
        private void CreateFloorplanGrid()
        {
            Dimension dimension = new Dimension(12, 50);
            GridService.CreateGridColumnDefinitions(FloorplanGrid, dimension);
            GridService.CreateGridRowDefinitions(FloorplanGrid, dimension);
            FillFloorplanGrid();
        }        

        private void FillFloorplanGrid()
        {
            foreach (Space space in spaces)
            {
                AddCell(space.XCoord, space.YCoord, GridService.Rooms.Find(r => r.Id == space.RoomId));
            }
        }

        private void AddCell(int column, int row, DrawableRoom room)
        {
            BoxView box = new BoxView { BackgroundColor = room.Color };

            switch (room.Type)
            {
                case RoomType.Kitchen:
                    AddKitchenTapEventToBox(box);
                    break;
                case RoomType.Workspace:
                    AddRoomTapEventToBox(box);
                    break;
            }

            GridService.AddItemToGridAtLocation(box, FloorplanGrid, row, column);
        }

        private void AddRoomTapEventToBox(BoxView box)
        {
            AddTapGestureRecognizer(box, RoomClicked);
        }

        private void AddKitchenTapEventToBox(BoxView box)
        {
            AddTapGestureRecognizer(box, KitchenClicked);
        }

        private void AddTapGestureRecognizer(BoxView box, EventHandler handler)
        {
            TapGestureRecognizer box_tap = new TapGestureRecognizer();
            box_tap.Tapped += handler;
            box.GestureRecognizers.Add(box_tap);
        }

        private void RoomClicked(object sender, EventArgs e)
        {
            int id = -1;
            BoxView box = ((BoxView)sender);
            foreach (DrawableRoom r in GridService.Rooms)
            {
                if (box.BackgroundColor.Equals(r.Color))
                {
                    id = r.Id;
                }
            }
            if (id != -1)
            {
                GridService.StoreSpacesFromSelectedRoom(id, spaces);
                FloorplanViewModel vm = ViewModelLocator.Instance.FloorplanViewModel;
                vm.RoomCommand.Execute(GridService.SelectedSpaces);
            }
        }

        private void KitchenClicked(object sender, EventArgs e)
        {
            BoxView box = ((BoxView)sender);
            DisplayAlert("Kitchen", "Kitchen information", "OK");
        }
        #endregion       
    }
}