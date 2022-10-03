﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolutionToPlastic.Views
{
    /// <summary>
    /// Interaction logic for Inventions.xaml
    /// </summary>
    public partial class Inventions : UserControl
    {
        public Inventions()
        {
            InitializeComponent();
        }
    }

    public class InventionsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string captionsharon = "A Woman Invented a New Kind of Plastic That Biodegrades in Water! It’s a Step Oceans Are Crying For!\n\nSharon Barak. She worked at a plastic manufacturing company for 7 years, she left the company to help the world fight against plastic pollution! We all use plastic items in our everyday life, and now it’s probably impossible to imagine life without them. But conventional plastic is not eco-friendly and when thrown away it can stay in nature for decades and even centuries posing a threat for both animals and people. She set a goal to make a product that would feel, look, and function like plastic, and dissolve in water doing no harm to nature at the same time.And she did it!\nAfter 2 years of hardwork Sharon and her team did it!\n\nIt consists of 100% eco-friendly materials that easily dissolve in water and become part of nature. The product is so safe and natural, that you can even drink its water solution. If a bag made of this product accidentally gets into the ocean, it will become part of it in just a few minutes, posing no threat to sea animals, unlike an ordinary plastic bag.";

        public string Caption_Sharon
        {
            get { return captionsharon; }
        }

        private string ohocaption = "Ooho is an innovative alternative solution to the classic plastic bottles and to mitigate container contamination. It consists of “spherified” water in a fine ecological and edible membrane that allows drinking without generating waste. And behind it there is an important Spanish touch. One of his co-inventors is Rodrigo García González who, together with his partner Pierre-Yves Paslier and a team in London where other Spanish designers are also, wants to revolutionize the world of bottled water with Ooho.\n\nThe design and concept of Ooho have already won numerous awards and their spheres have even been used in various events. Now they want to market them to try to help reduce the environmental impact generated by plastic bottles. Although some voices point out that their ability to be an alternative is limited because the spheres degrade in a few days.";

        public string OhoCaption
        {
            get { return ohocaption; }
        }
    }
}
