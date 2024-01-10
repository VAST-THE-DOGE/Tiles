package Tiles;

import javax.imageio.ImageIO;
import javax.print.DocFlavor.STRING;
import javax.swing.ImageIcon;
import javax.swing.JButton;
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
import Tiles.tile;

public class Main extends JFrame {

    public static final boolean DEBUG = true;

    public static final String VERSION = "0.0.1";

    private static final int WIDTH = 1080;
    private static final int HEIGHT = 960;

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
        JPanel p = new JPanel();
        f.setSize(WIDTH,HEIGHT);
        f.setContentPane(p);
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        //load the icon
        BufferedImage img = null;
        try {
            img = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/TilesLogoV2.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        f.setIconImage(img);       
        //f.add(game);
        System.out.println("- Window setup is complete");

        //in game loading setup WIP
        int LoadID = 0;

        //load map
        tile[][] map = game.createTileMap(game.loadMap(LoadID));

        //load resources
        playerResources = game.loadResources(LoadID);

        //setup window
        f.getContentPane().setLayout(new GridLayout(10,20));
        JButton[][] buttons = new JButton[10][20];
        for(int j = 0; j < 10; j++)
        {
            for(int i = 0; i < 20; i++)
            {
                buttons[j][i] = new JButton();
                

                buttons[j][i].setIcon(new ImageIcon(map[j][i].imageFile.getScaledInstance(95, 95, Image.SCALE_SMOOTH)));
                int column = i;
                int row = j;
                buttons[j][i].addActionListener(e -> game.clicked(row,column));
                f.getContentPane().add(buttons[j][i]);
            }
        }
        f.pack();
        f.setVisible(true);

        System.out.println("- game window setup is complete");

        //game loop WIP
        while(true)
        {
            //manage the gui and tiles.
            //use a separate script for keeping track of time.
            //save every 24 seconds or every in game day.
            for(int j = 0; j < 10; j++)
            {
            for(int i = 0; i < 20; i++)
            {
                buttons[j][i] = new JButton();

                buttons[j][i].setIcon(new ImageIcon(map[j][i].imageFile.getScaledInstance(buttons[j][i].getWidth(), buttons[j][i].getHeight(), Image.SCALE_SMOOTH)));

            }
            }
            //f.setVisible(false);
            //f.setVisible(true);
            }
    }

    private void clicked(int row, int column)
    {
        //wip
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

    private tile[][] createTileMap(Integer[][] map)
    {
        //defaults
        tile[] layer0 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer1 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer2 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer3 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer4 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer5 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer6 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer7 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer8 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[] layer9 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        tile[][] tileMap = {layer0, layer1, layer2, layer3, layer4, layer5, layer6, layer7, layer8, layer9};
        
        //create tiles
        for(int j = 0; j < 10; j++)
        {
            for(int i = 0; i < 20; i++)
            {
                tileMap[j][i] = new Tiles.tile(map[j][i]);
            }
        }
        
        return tileMap;
    }

    private Integer[] loadResources(int ID)
    {
        return playerResources;
    }

    private Integer[][] loadMap(int ID)
    {
        Integer[] layer0 = {0,0,0,0,7,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer1 = {0,0,7,1,12,13,13,13,13,0,0,0,0,7,1,0,0,0,0,0};
        Integer[] layer2 = {0,7,12,13,13,13,18,13,13,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer3 = {0,2,13,15,13,13,13,13,13,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer4 = {0,2,16,13,16,17,13,13,13,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer5 = {0,2,13,15,16,13,13,13,13,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer6 = {0,8,3,9,13,13,13,10,0,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer7 = {0,0,0,8,3,3,3,5,0,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer8 = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        Integer[] layer9 = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        Integer[][] map = {layer0, layer1, layer2, layer3, layer4, layer5, layer6, layer7, layer8, layer9};
        return map;
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


}
