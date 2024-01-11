package Tiles;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import java.awt.Color;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.Scanner;
import java.lang.Thread;


public class Main extends JFrame {

    public static final boolean DEBUG = true;

    public static final String VERSION = "0.0.2";

    private static final int WIDTH = 1080;
    private static final int HEIGHT = 960;

    private static final int MAX_TILES = 31;

    //menu stuff.

    private static final int[] BR1 = {4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,7};
    private static final int[] BR2 = {0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,2};
    private static final int[] BR3 = {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6};

    private static final int[] RC3 = {7,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
    private static final int[] RCM = {3,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8};
    private static final int[] RC1 = {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    //////////////////////////////////////////////////////////////////////

    private static Tile[][] map;

    private static int[] selected = {0,0};

    private static JButton[][] buttons = new JButton[20][40];

    private static ImageIcon[] tileIcons = new ImageIcon[MAX_TILES+1];

    private static ImageIcon[] menuIcons = new ImageIcon[9];

    private static BufferedImage NO_IMAGE_ICON;

    // day, hour
    private static int[] time = {0,0};

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

        //load the no image icon!
        try {
            NO_IMAGE_ICON = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/NoImageIcon.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        


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
        int LoadID = 1;

        //load map
         map = game.createTileMap(game.loadMap(LoadID));

        //load resources
        playerResources = game.loadResources(LoadID);

        //setup window
        f.getContentPane().setLayout(new GridLayout(20,40));
        
        for(int j = 0; j < 20; j++)
        {
            for(int i = 0; i < 40; i++)
            {
                buttons[j][i] = new JButton();
                
                int column = i;
                int row = j;
                buttons[j][i].setBackground(Color.BLACK);
                buttons[j][i].addActionListener(e -> game.clicked(row,column));
                f.getContentPane().add(buttons[j][i]);
            }
        }
        f.pack();
        f.setVisible(true);

        System.out.println("- game window setup is complete");

        double startTime = 0;

        //game loop setup
        int oldHeight = f.getHeight();
        int oldWidth = f.getWidth();

        BufferedImage imageFile;
        for(int id = 0; id<=MAX_TILES; id++)
        {
            imageFile = NO_IMAGE_ICON;
            try {
            imageFile = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/Tile"+id+".png"));
            } catch (IOException e) {
            e.printStackTrace();
            }
            tileIcons[id] = new ImageIcon(imageFile.getScaledInstance((int)(50), (int)(50), Image.SCALE_SMOOTH));
        }
        for(int id = 0; id<=8; id++)
        {
            imageFile = NO_IMAGE_ICON;
            try {
            imageFile = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/menu"+id+".png"));
            } catch (IOException e) {
            e.printStackTrace();
            }
            menuIcons[id] = new ImageIcon(imageFile.getScaledInstance((int)(50), (int)(50), Image.SCALE_SMOOTH));
        }
        game.updateWindow(NO_IMAGE_ICON);

        //game loop
        while(true)
        {
            try {
                Thread.sleep(50);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            if(startTime < System.currentTimeMillis())
            {
                //manage time
                time[1] += 1;
                startTime = System.currentTimeMillis()+1000;
                if(time[1]>24)
                {
                    time[1] = 1;
                    time[0] += 1;

                    //save game
                    game.saveWorld();
                }

            //check for window changes
                if(oldHeight!=f.getHeight() || oldWidth!=f.getWidth())
                {
                    //update tiles.
                    oldHeight = f.getHeight();
                    oldWidth = f.getWidth();

                    Runnable runnable = () -> {
                    // Call your function here
                    game.updateWindow(NO_IMAGE_ICON);
                    };
                    // Create a Thread object with your Runnable object
                    Thread thread = new Thread (runnable);
                    // Start the new thread
                    thread.start ();
                    
                    //game.updateWindow(NO_IMAGE_ICON);
                }
            //rest of stuff


                System.out.println(time[0]+" Days and "+time[1]+" Hours.");
            }
            }
        }


    private void clicked(int row, int column)
    {


        if(row<17&&column<33)
        {
        buttons[selected[0]][selected[1]].setIcon(tileIcons[map[selected[0]][selected[1]].id]);
        buttons[selected[0]][selected[1]].setBackground(Color.BLACK);
        buttons[row][column].setIcon(new ImageIcon(map[row][column].imageFile.getScaledInstance((int)(buttons[0][0].getWidth()/1.2), (int)(buttons[0][0].getHeight()/1.2), Image.SCALE_SMOOTH)));
        buttons[row][column].setBackground(Color.RED);


        System.out.println("clicked");
        System.out.println(map[row][column]+" at ("+row+", "+column+").");
        selected[0] = row;
        selected[1] = column;
        }
        else
        {
            //TODO
        }

    }

    public void updateWindow(BufferedImage imageFile)
    {
        try
        {
        //update tiles.
        for(int id = 0; id<=MAX_TILES; id++)
        {
            imageFile = NO_IMAGE_ICON;
            try {
                imageFile = ImageIO.read(getClass().getResourceAsStream("Data/ImageData/Tile"+id+".png"));
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            tileIcons[id] = new ImageIcon(imageFile.getScaledInstance(buttons[0][0].getWidth(), buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
        }
        for(int id = 0; id<=8; id++)
        {
            imageFile = NO_IMAGE_ICON;
            try {
            imageFile = ImageIO.read(getClass().getResourceAsStream("Data/ImageData/menu"+id+".png"));
            } catch (IOException e) {
            e.printStackTrace();
            }
            menuIcons[id] = new ImageIcon(imageFile.getScaledInstance((int)buttons[0][0].getWidth(), (int)buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
        }

        for(int i = 0; i < 17; i++)
        {
            for(int j = 0; j < 33; j++)
            {                 
                if(selected[0] == i && selected[1] == j)
                {
                    buttons[i][j].setBackground(Color.RED);
                        
                    buttons[i][j].setIcon(new ImageIcon(map[i][j].imageFile.getScaledInstance((int)(buttons[0][0].getWidth()/1.2), (int)(buttons[0][0].getHeight()/1.2), Image.SCALE_SMOOTH)));
                }
                else
                {
                    buttons[i][j].setBackground(Color.BLACK);
                        
                    buttons[i][j].setIcon(tileIcons[map[i][j].id]);
                }
            }
        }
        //update the menu.

        //bottom menu
        for(int i = 0; i < 40; i++)
        {
            buttons[17][i].setIcon(menuIcons[BR1[i]]);
            buttons[18][i].setIcon(menuIcons[BR2[i]]);
            buttons[19][i].setIcon(menuIcons[BR3[i]]);
        }

        //right menu
        for(int i = 0; i<17; i++)
        {
            buttons[i][33].setIcon(menuIcons[RC1[i]]);
            for(int j = 34; j<39; j++)
            {
                buttons[i][j].setIcon(menuIcons[RCM[i]]);
            }
            buttons[i][39].setIcon(menuIcons[RC3[i]]);
        }

        }
        catch(Exception e)
        {
            System.err.println("!!!MAP UPDATE ERROR!!!");
        }
    }

    private void saveWorld()
    {
        //wip
        System.err.println("Saving worlds are not implemented yet!");
    }

    private void generateWorld()
    {
        //WIP
        System.err.println("generating worlds are not implemented yet!");
    }

    private Tile[][] createTileMap(Integer[][] map)
    {
        //defaults
        Tile[] layer0 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer1 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer2 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer3 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer4 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer5 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer6 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer7 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer8 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer9 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer10 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer11 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer12 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer13 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer14 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer15 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[] layer16 = {null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null};
        Tile[][] tileMap = {layer0, layer1, layer2, layer3, layer4, layer5, layer6, layer7, layer8, layer9, layer10, layer11, layer12, layer13, layer14, layer15, layer16};
        
        //create tiles
        for(int j = 0; j < 17; j++)
        {
            for(int i = 0; i < 33; i++)
            {
                tileMap[j][i] = new Tile(map[j][i]);
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
        Integer[][] map = new Integer[17][34];
        try 
        {
        System.out.println("Loading World "+ID);
        Scanner scanner = new Scanner(getClass().getResourceAsStream("SavedWorlds/World"+ID+".txt"));
        scanner.nextLine();
        scanner.nextLine();

        //TODO player resources for loop!
        scanner.nextLine(); //temp



        scanner.nextLine();

        //TODO time for loop!
        scanner.nextLine(); //temp



        scanner.nextLine();

        for(int i = 0; i<17; i++)
        {
            for(int j = 0; j<34; j++)
            {
                map[i][j] = scanner.nextInt();
                System.out.print(map[i][j]+" ");
            }
            scanner.nextLine();
            System.out.println();
        }
        scanner.close();
        System.out.println("World Loaded!");
        }

        catch(Exception e)
        {
            System.err.println("!!!WORLD LOAD ERROR!!!");
        }


        return map;

    }

    /**a way to upgrade tiles
     * @param T this is the old tile.
     * @param selection this is which upgrade was selected and will be 1, 2, or 3.
     * the others are the location
    */
    private void upgradeTile(int row, int column, Tile T, int selection)
    {
        //WIP

        //get tile data and find the into 1, 2, or 3 cost and tile.
        Integer[] cost = {0,0,0,0,0,0,0};
        Integer newID = -1;
        switch (selection) {
            case 1:
                cost = T.cost1;
                newID = T.Upgrades[0];

            case 2:
                cost = T.cost2;
                newID = T.Upgrades[1];
                

            case 3:
                cost = T.cost3;
                newID = T.Upgrades[2];
    
        //remove resources
        if(resourceCheck(cost))
        {
        System.out.println("Resource check passed for upgrade to "+newID+" for tile at ("+row+", "+column+").");
        for(int i=0; i<7; i++)
        {
            playerResources[i] -= cost[i];
        }

        //update tile
        T.update(newID);
        }
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
        System.out.println("Tile upgraded at ("+row+", "+column+") to "+T.id+"!");

        }
        else System.out.println("Resource check failed for upgrade to "+newID+" for tile at ("+row+", "+column+").");
    }
    private boolean resourceCheck(Integer[] other)
    {
        for(int i = 0; i<7; i++)
        {
            if(playerResources[i] - other[i] < 0)
            {
                return false;
            }
        }
        return true;
    }

class Tile
    {
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

        Tile(Integer tileID)
        {
            //use the id to set everything else
            this.update(tileID);
        }
        void update(Integer newID)
        {
            this.id = newID;
            //get new info

            //update the image.
            imageFile = NO_IMAGE_ICON;
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

    
}
