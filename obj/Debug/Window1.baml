   M S B A M L   `   `   `  ����Z  VHelixToolkit.Wpf, Version=2014.2.1.1, Culture=neutral, PublicKeyToken=52aa3500039caf0dM8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.WpfHelixToolkit.Wpf  5      J FSolarsystemDemo, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SolarsystemDemoSolarsystemDemo 5      1clr-namespace:SolarsystemDemoSolarsystemDemo 5   	      SolarsystemDemo.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jhelix8clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Wpf     5      +sclr-namespace:SolarsystemDemo     5      /localclr-namespace:SolarsystemDemo     5   	     � Title$  SolarSystemDemo��5      $	��480��6   $	��640��6*    � 	Resources 5
      $  SolarsystemDemo.CameraMaxValue   CameraMaxValueConverter��%   &	          5   
   6A   5      ��5      �� 5   
    �� ImageSource$  Textures/skybox/cube_b.jpg��6    j� TileMode$ FlipX=�6>    j� Viewport$ 0,0, 0.5,0.5�6O   .��5      ]� .+�5   
   ]� $	��Top=�6   $��8q�6)   .+�5      �� 
�������$	��140��6W   $��Center=�6c   $��Right=�6�   ��6   �� *Time+ �� StringFormat$ {0:u}��6�   �� 5      -   5        $��
TimeSlider��5      $	��Top=�6'    � Minimum$ 0Q�6S    � Maximum$	 365Q�6_    � Value 6<   �� *Days+6_   �� 5      -   5        	 ��Name$	 	EarthView��5      $��
Earth View��6*   $��Bottom=�6[   
�������$��0,5,5,0q�6�   6�   5      &    HelixToolkit.Wpf.HelixViewport3D  5   
   -   5        
  Name$
 view1��5          InfiniteSpin .     ShowCameraInfo .     ShowFieldOfView .     ShowCoordinateSystem .    Children. 5      g�  g� Content 5      h� 5      ��5      "�  !� Position$ 0,0,01�6)    �� Color$ White��6:   �� 5      $ #101010��6+   5      5!      #  SolarsystemDemo.SolarSystem3D  5#         DistanceScale$	 1e6Q�6      DiameterScale$	 1e3Q�62   .2�5$        SolarsystemDemo.Planet3D  &   SolarsystemDemo.TexturedObject3D   
ObjectName$	 Sun��6      
MeanRadius$ 34800Q�6.      RotationPeriod$ 	25.379995Q�6A      
foreground
 ����    
background
 ���� 6u     5%      $ Mercury��6      Aphelion$ 69816900Q�5&   %      
Perifelion$ 46001200Q�69      SemiMajorAxis$ 57909100Q�5'   %      Eccentricity$ 0.205630Q�6>      OrbitalPeriod$ 87.969 Q�5(   %      Inclination$
 6.34Q�5)   %       LongitudeOfAscendingNode$  48.331Q�68   !   ArgumentOfPerihelion$! 29.124Q�6Z   $ 2439.7Q�5*   %   $ 58.6462Q�69   
 �����
 ���� "   borderbrush
" ���� #   borderthickness$# 0q�5+   k   $   height$	$ 1e4Q�6   %   padding$% 0q�6�   &   depthoffset$& 0Q�6�   '   font$' Courier New)�6�     5,      $ Venus��6   $ 	108942109Q�5-   %   $ 	107476259Q�6:   $ 	108208930Q�5.   %   $ 0.0068Q�6?   $ 	224.70069Q�5/   %   $
 2.19Q�50   %   $  76.67069Q�68   $! 54.85229Q�6\   $ 6051.8Q�51   %   $ 	-243.0187Q�69   
 �����
 ���� 
" ���� $# 1q�52   k   $	$ 1e4Q�6   $% 2q�6�   $	& 1e2Q�6�   $' Courier New)�6�     53      $ Earth��6   $ 	152097701Q�54   %   $ 	147098074Q�6:   $ 149597887.5Q�55   %   $ 0.016710219Q�6A   $ 
365.256366Q�56   %   $ 1.57869Q�57   %   $  	348.73936Q�6;   $! 	114.20783Q�6`   $ 6371.0Q�58   %   (   	AxialTilt$( 	23.439281Q�59   %   $ 
0.99726968Q�6;   )   DistanceScale$	) 3e4Q�5:   %   *   DiameterScale$	* 1e3Q�69   
 �����
 ���� 
" ���� $# 1q�5;   k   $	$ 1e4Q�6   $% 2q�6�   $
& 1e-3Q�6�   $' Courier New)�6�   +   
Satellites+ 5>      !  SolarsystemDemo.Satellite3D  $
 Moon��6(   ,   Perigee$, 363104Q�6:   -   Apogee$- 405696Q�6K   .   SemiMajorAxis$. 384399Q�6[   /   Eccentricity$/ 0.0549Q�5?   *   0   OrbitalPeriod$0 	27.321582Q�6@   1   Inclination$1 5.145Q�6Z   $ 1737.10Q�5@   *   2   	AxialTilt$2 6.687Q�5A   *   3   RotationPeriod$3 	27.321582Q�6<   5D        5F      $
 Mars��6   $ 0Q�5G   %   $ 0Q�62   $ 	227939100Q�5H   %   $ 0.093315Q�6?   $ 686.971Q�5I   %   $
 1.67Q�5J   %   $  49.562Q�68   $! 286.537Q�6Z   $ 3389.5Q�5K   %   $ 
1.02595675Q�69   $	) 2e3Q�5L   %   $	* 1e1Q�69   
 �����
 ���� 
" ���� $# 1q�5M   k   $	$ 1e4Q�6   $% 2q�6�   $	& 1e2Q�6�   $' Courier New)�6�   + 5O        $ Phobos��6(   $, 0Q�6<   $- 0Q�6H   $. 9377.2Q�6S   $/ 0.0151Q�5P   *   $0 
0.31891023Q�6@   $1 26.04Q�6[   $
 11.1Q�5Q   *   $2 0Q�6<   $3 0Q�6J     5R      $ Deimos��6(   $, 0Q�6<   $- 0Q�6H   $. 23460Q�6S   $/ 0.0002Q�5S   *   $0 1.26244Q�6@   $1 27.58Q�6X   $	 6.2Q�5T   *   $2 0Q�6;   $3 0Q�6I   5W        5X      $ Jupiter��6   $ 0Q�5Y   %   $ 0Q�62   $ 	778547200Q�5Z   %   $ 0.048775Q�6?   $ 4331.572Q�5[   %   $
 0.32Q�5\   %   $  100.492Q�68   $! 275.066Q�6[   $ 35746Q�5]   %   $ 0.41007Q�68   
 �����
 ���� 
" ���� $# 1q�5^   k   $	$ 1e4Q�6   $% 2q�6�   $
& 1e-3Q�6�   $' Arial)�6�     5_      $ Saturn��6   $ 0Q�5`   %   $ 0Q�62   $ 
1433449370Q�5a   %   $ 0.055723219Q�6@   $ 10759.22Q�5b   %   $
 0.93Q�5c   %   $  
113.642811Q�68   $! 
336.013862Q�6^   $ 30134Q�5d   %   $ 0.426Q�68   
 �����
 ���� 
" ���� $# 1q�5e   k   $	$ 1e4Q�6   $% 2q�6�   $
& 1e-3Q�6�   $' Arial)�6�   5f        5h      $ Uranus��6   $ 0Q�5i   %   $ 0Q�62   $ 
2876679082Q�5j   %   $ 0.044405586Q�6@   $ 	30799.095Q�5k   %   $
 1.02Q�5l   %   $  	73.989821Q�68   $! 	96.541318Q�6]   $ 12800Q�5m   %   $ -0.71833Q�68   
 �����
 ���� 
" ���� $# 1q�5n   k   $	$ 1e4Q�6   $% 2q�6�   $
& 1e-3Q�6�   $' Arial)�6�     5o      $ Neptune��6   $ 0Q�5p   %   $ 0Q�62   $ 
4503443661Q�5q   %   $ 0.011214269Q�6@   $ 60190Q�5r   %   $
 0.72Q�5s   %   $  
131.794310Q�68   $! 
265.646853Q�6^   $ 20000Q�5t   %   $ 0.67125Q�68   
 �����
 ���� 
" ���� $# 1q�5u   k   $	$ 1e4Q�6   $% 2q�6�   $
& 1e-3Q�6�   $' Arial)�6�   5w         HelixToolkit.Wpf.SunLight  5x      /   )HelixToolkit.Wpf.CoordinateSystemVisual3D  5y      1	   +HelixToolkit.Wpf.BillboardTextGroupVisual3D	  5z      -   5        pinBoard��4 	  	IsEnabled4 . 5 	  BorderThickness$5 0,2,0,0q�5{   0   6 	  Padding$6 1,1,3,1q�6J   7 	  Offset$7 50,50-�6\   8 	  FontSize$8 20Q�6�   9 	  PinBrush
9 �����: 	  BorderBrush
: �����; 	  PinWidth$; 2Q�6�   < 	  
Foreground
< � ���= 	  
FontFamily$= Courier New)�6�   > 	  Items> 6k   �� *
TextItem+6�   %
   HelixToolkit.Wpf.SphereVisual3D
  5|      ? 
  Center$? 	100,100,01�6#   @ 
  Radius$@ 3Q�66   $   HelixToolkit.Wpf.MeshElement3DA   Fill
A � ���6A   %   HelixToolkit.Wpf.PanoramaCube3D  5}      -   5        panoramaCube3D��B   Source$B Textures\skybox\��5}   ;   C   SizeC 6U   �� *Camera+D �� ElementName$D view1��E �� 	Converter#E [  5~      5      5�      