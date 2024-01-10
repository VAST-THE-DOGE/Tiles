package Tiles;

import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.Scanner;

import javax.imageio.ImageIO;

public class tile
    {
        //tile image
        BufferedImage imageFile = null;

        //used to get tile data
        Integer id = -1;

        //tile name
        String name = "ERROR";

        //index = resource. 0= gold, 1= iron, 2= stone, 3= wood, 4= water, 5= food, 6= population.
        Integer[] ResourceGain = {0,0,0,0,0,0,0};
        Integer[] ResourceLoss = {0,0,0,0,0,0,0};

        //upgrades
        //id of tile to upgrade to
        Integer[] Upgrades = {0,0,0};
        Integer[] cost1 = {0,0,0,0,0,0,0};
        Integer[] cost2 = {0,0,0,0,0,0,0};
        Integer[] cost3 = {0,0,0,0,0,0,0};

        tile(Integer tileID)
        {
            //use the id to set everything else
            this.update(tileID);
        }
        void update(Integer newID)
        {
            this.id = newID;
            //get new info

            //update the image
            imageFile = null;
            try {
            imageFile = ImageIO.read(getClass().getResourceAsStream("Data/ImageData/Tile"+newID+".png"));
            } catch (IOException e) {
            e.printStackTrace();
            }

            //get the tile info
            try {
                    Scanner scanner = new Scanner(getClass().getResourceAsStream("Data/TileData/"+newID+".txt"));
                    
                    scanner.nextLine();
                    name = scanner.nextLine();
                    scanner.nextLine();
                    scanner.nextLine();
                    //ResourceGain
                    for(int i = 0; i<7; i++)
                    {
                        ResourceGain[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    scanner.nextLine();
                    scanner.nextLine();
                    //ResourceLoss
                    for(int i = 0; i<7; i++)
                    {
                        ResourceLoss[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    scanner.nextLine();
                    //Upgrades
                    for(int i = 0; i<3; i++)
                    {
                        Upgrades[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    scanner.nextLine();
                    //cost1
                    for(int i = 0; i<7; i++)
                    {
                        cost1[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    scanner.nextLine();
                    //cost2
                    for(int i = 0; i<7; i++)
                    {
                        cost2[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    scanner.nextLine();
                    //cost3
                    for(int i = 0; i<7; i++)
                    {
                        cost3[i] = scanner.nextInt();
                    }
                    scanner.close();
            } catch (Exception e) {
                System.err.println("!!!Tile load error!!!");
            }
        }
    }
