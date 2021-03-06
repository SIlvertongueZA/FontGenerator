﻿using System;
using System.Collections.Generic;
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

namespace FontGenerator
{
  /// <summary>
  /// Interaction logic for LCDCanvas.xaml
  /// </summary>
  public partial class LCDCanvas : UserControl
  {
    private DisplayByte[,] bytes = new DisplayByte[8,128];

    public byte[,] Value
    {
      get
      {
        byte[,] temp = new byte[_height, _width];

        for(int i = 0; i < _height; i++ )
        {
          for(int j = 0; j < _width; j++ )
          {
            temp[i, j] = bytes[i, j].Value;
          }
        }
        return temp;
      }

      set
      {
        Height = value.GetLength(0);
        Width = value.GetLength(1);

        for(int i = 0; i < _height; i++ )
        {
          for(int j = 0; j < _width; j++ )
          {
            bytes[i, j].Value = value[i, j];
          }
        }
        Paint();
      }
    }

    private int _height = 8;
    public int y
    {
      get { return _height; }
      set
      {
        DisplayByte[,] temp = new DisplayByte[_height, _width];
        Array.Copy(bytes, temp, _height * _width);

        bytes = new DisplayByte[value, _width];

        for(int i = 0; i < value; i++ )
        {
          for(int j = 0; j < _width; j++ )
          {
            if ( (i >= _height) || (j >= _width) )
              bytes[i, j] = new DisplayByte();
            else
              bytes[i, j] = temp[i, j];
          }
        }
        _height = value;
        Paint();
      }
    }

    private int _width = 128;

    new public int x
    {
      get { return _width; }
      set
      {
        DisplayByte[,] temp = new DisplayByte[_height, _width];
        Array.Copy(bytes, temp, _height * _width);

        bytes = new DisplayByte[_height, value];

        for(int i = 0; i < _height; i++ )
        {
          for(int j = 0; j < value; j++ )
          {
            if ( (i >= _height) || (j >= _width) )
              bytes[i, j] = new DisplayByte();
            else
              bytes[i, j] = temp[i, j];
          }
        }
        _width = value;
        Paint();
      }
    }

    public LCDCanvas()
    {
      InitializeComponent();

      _width = 128;
      _height = 8;

      for(int i = 0; i < 8; i++ )
      {
        for(int j = 0; j < 128; j++ )
        {
          bytes[i, j] = new DisplayByte();
        }
      }
      Paint();
    }

    public void Paint()
    {
      canvasGrid.Children.Clear();
      mapGrid.Children.Clear();

      mapGrid.Rows = _height;
      canvasGrid.Rows = _height;
      mapGrid.Columns = _width;
      canvasGrid.Columns = _width;

      for(int i = 0; i < _height; i++ )
      {
        for(int j = 0; j < _width; j++ )
        {
          canvasGrid.Children.Add(bytes[i, j]);
          mapGrid.Children.Add(bytes[i, j].Text);
        }
      }
    }

    public void Invert()
    {
      foreach(DisplayByte db in bytes)
      {
        db.Invert();
      }
    }

  }
}
