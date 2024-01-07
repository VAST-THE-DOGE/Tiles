package GameTest;

import javax.imageio.ImageIO;
import javax.print.DocFlavor.STRING;
import javax.swing.JFrame;
import javax.swing.JPanel;
import java.awt.Color;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Scanner;
import java.awt.Color;

public class Main extends JFrame {

    public static final boolean DEBUG = true;

    public static final String VERSION = "0.0.1";

    private static final int WIDTH = 1500;
    private static final int HEIGHT = 750;

    //index = resource. 0= gold, 1= iron, 2= stone, 3= wood, 4= water, 5= food, 6= population.
    private static Integer[] playerResources = {0,0,0,10,30,20,100};

    public static void main(String[] args)
    {
        //print debug stuff
        System.out.println("/////////////////////////////////////////\n//  Tiles by William Herbert           //\n//  Version  "+VERSION+"                     //\n/////////////////////////////////////////");
        System.out.println("-\n- loading...");
        // menu setup WIP
        System.out.println("-\n- Setting up window");
        Main game = new Main();
        JFrame f = new JFrame("Tiles " + VERSION);
        f.setSize(WIDTH,HEIGHT);
        f.setVisible(true);
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        //load the icon
        BufferedImage img = null;
        try {
            img = ImageIO.read(game.getClass().getResourceAsStream("Data/imageData/TilesLogoV2.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        f.setIconImage(img);       
        //f.add(game);
        System.out.println("- Window setup is complete");

        //in game loading setup WIP
        f.getContentPane().setLayout(new GridLayout(10,20));

        //game loop WIP
        while(true)
        {
            //manage the gui and tiles.
            //use a separate script for keeping track of time.
            //save every 24 seconds or every in game day.
        }
    }

    private void saveWorld()
    {
        //wip
        System.err.println("Saving worlds are not working!");
    }

    private void generateWorld()
    {
        //WIP
        System.err.println("generating worlds are not working!");
    }

    /**a way to upgrade tiles
     * @param oldT this is the old tile.
     * @param selection this is which upgrade was selected and will be 1, 2, or 3.
     * the others are the location
    */
    private void upgradeTile(int row, int column, tile T, int selection)
    {
        //WIP

        //get tile data and find the into 1 2 or 3 cost and tile.
        Integer[] cost = {0,0,0,0,0,0,0};
        Integer newID = -1;
        switch (selection) {
            case 1:
                cost = T.cost1;
                newID = T.Upgrades[0];
                break;

            case 2:
                cost = T.cost2;
                newID = T.Upgrades[1];
                break;

            case 3:
                cost = T.cost3;
                newID = T.Upgrades[2];
                break;
        }
        //check the cost and make sure the player has the required resources. use "player resource >= cost" if statements!
        if(resourceCheck(cost))
        {
        //remove resources.
        for(int i = 0; i!=7; i++)
        {
            playerResources[i] -= cost[i];
        }
        //update the tile.
        T.update(newID);
        }
    }
    private boolean resourceCheck(Integer[] other)
    {
        for(int i = 0; i!=7; i++)
        {
            if(playerResources[i] - other[i] < 0)
            {
                return false;
            }
        }
        return true;
    }

    private class tile
    {
        //tile image
        private BufferedImage imageFile = null;

        //used to get tile data
        private Integer id = -1;

        //tile name
        private String name = "ERROR";

        //index = resource. 0= gold, 1= iron, 2= stone, 3= wood, 4= water, 5= food, 6= population.
        private Integer[] ResourceGain = {0,0,0,0,0,0,0};
        private Integer[] ResourceLoss = {0,0,0,0,0,0,0};

        //upgrades
        //id of tile to upgrade to
        private Integer[] Upgrades = {0,0,0};
        private Integer[] cost1 = {0,0,0,0,0,0,0};
        private Integer[] cost2 = {0,0,0,0,0,0,0};
        private Integer[] cost3 = {0,0,0,0,0,0,0};

        tile(Integer tileID)
        {
            //use the id to set everything else
            this.update(tileID);
        }
        private void update(Integer newID)
        {
            this.id = newID;
            //get new info

            //update the image
            BufferedImage imageFile = null;
            try {
            imageFile = ImageIO.read(getClass().getResourceAsStream("Data/imageData/Tile"+newID+".png"));
            } catch (IOException e) {
            e.printStackTrace();
            }

            //get the tile info
            try {
                    Scanner scanner = new Scanner(getClass().getResourceAsStream("Data/tileData/"+newID+".txt"));
                    
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
                    //ResourceLoss
                    for(int i = 0; i<7; i++)
                    {
                        ResourceLoss[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    //Upgrades
                    for(int i = 0; i<3; i++)
                    {
                        Upgrades[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    //cost1
                    for(int i = 0; i<7; i++)
                    {
                        cost1[i] = scanner.nextInt();
                    }
                    scanner.nextLine();
                    //cost2
                    for(int i = 0; i<7; i++)
                    {
                        cost2[i] = scanner.nextInt();
                    }
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
}