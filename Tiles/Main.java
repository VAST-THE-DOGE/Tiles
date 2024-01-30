package Tiles;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.Point;
import java.awt.image.BufferedImage;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;
import java.lang.Thread;

import java.nio.charset.StandardCharsets;



public class Main extends JFrame {

    public static final boolean DEBUG = true;

    public static final String VERSION = "0.2.0";

    private static final int MAX_TILES = 68;

    //menu stuff.

    private static final int[] BR1 = {4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,10,3,3,3,3,3,9};
    private static final int[] BR2 = {0,12,14,13,8,12,14,14,13,8,12,14,14,13,8,12,14,14,13,8,12,14,14,13,8,12,14,14,13,8,12,14,14,13,8,12,14,14,13,2};
    private static final int[] BR3 = {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,6};

    private static final int[] RC3 = {7,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
    private static final int[] RCM = {3,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8};
    private static final int[] RC1 = {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    //////////////////////////////////////////////////////////////////////
    //Settings. WIP, will be loaded from Settings.txt in a future update.
    private static int audio = 100;
    private static int menuUpdateTick = 100;
    private static int gameUpdateTick = 50;
    private static boolean savePerHour = true;
    private static boolean grid = true;
    private static boolean potatoMode = false;
    private static boolean tinyGUIMode = false;
    private static boolean cheatsEnabled = false;
    //////////////////////////////////////////////////////////////////////

    private static Tile[][] map;

    private static int[] selected = {0,0};

    private static JButton[][] buttons = new JButton[20][40];

    private static ImageIcon[] tileIcons = new ImageIcon[MAX_TILES+1];

    private static ImageIcon[] menuIcons = new ImageIcon[30];

    private BufferedImage NO_IMAGE_ICON;

    private static boolean inMainMenu = true;

    private static int loadID = 1;

    private static int popDebuff = 0;
    
    // day, hour
    private static int[] time = {0,0};

    //game speed
    private static int speed = 0;

    //index = resource. 0= gold, 1= iron, 2= stone, 3= wood, 4= water, 5= food, 6= population.
    private static int[] playerResources = {0,0,0,0,0,0,100};

    private static int[] resourceChange = {0,0,0,0,0,0,0};

    public static void main(String[] args)
    {

        System.out.println("/////////////////////////////////////////\n//  Tiles by William Herbert           //\n//  Version  "+VERSION+"                     //\n/////////////////////////////////////////");
        System.out.println("- For bug reporting, updates, and useful info,\n- go to https://github.com/VAST-THE-DOGE/Tiles");
        System.out.println("-\n- loading...");
        // menu setup WIP
        System.out.println("-\n- Setting up window");
        Main game = new Main();
        JFrame f = new JFrame("Tiles " + VERSION);
        JPanel p = new JPanel();
        JPanel mainMenu = new JPanel();
        f.setContentPane(mainMenu);
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        //load the no image icon!
        try {
            game.NO_IMAGE_ICON = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/NoImageIcon.png"));
        } catch (IOException e) {
            System.err.println("!!!NoIconImage did not load!!!");
            e.printStackTrace();
        }

        //load the icon
        BufferedImage img = game.NO_IMAGE_ICON;
        try {
            img = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/TilesLogoV2.png"));
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e2) {System.err.println("Image read error!");}
        f.setIconImage(img);   

        BufferedImage buttonImg = game.NO_IMAGE_ICON;

        BufferedImage backgroundImg = game.NO_IMAGE_ICON;

        try {
            backgroundImg = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/Menu8.png"));
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e2) {System.err.println("Image read error!");}
        try {
            buttonImg = ImageIO.read(game.getClass().getResourceAsStream("Data/ImageData/Menu15.png"));
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e2) {System.err.println("Image read error!");}

        JLabel label;
        JButton buttonWL1;
        JButton buttonWL2;
        JButton buttonWL3;

        JButton buttonME;
        JButton buttonNA1;
        JButton buttonGH;
        JButton buttonS;

        //menuSetup
        mainMenu.setLayout(new GridLayout(4,1));

        label = new JLabel();
        label.setText("Tiles");
        label.setHorizontalTextPosition(JButton.CENTER);
        label.setVerticalTextPosition(JButton.CENTER);
        label.setFont(new Font("Arial", Font.BOLD, 100));

        buttonWL1 = new JButton();
        buttonWL1.addActionListener(e -> {inMainMenu = false; loadID = 1;});
        buttonWL1.setMargin(game.getInsets());
        buttonWL1.setText("Load World 1");
        buttonWL1.setHorizontalTextPosition(JButton.CENTER);
        buttonWL1.setVerticalTextPosition(JButton.CENTER);
        buttonWL1.setFont(new Font("Arial", Font.BOLD, 50));

        buttonWL2 = new JButton();
        buttonWL2.addActionListener(e -> {inMainMenu = false; loadID = 2;});
        buttonWL2.setMargin(game.getInsets());
        buttonWL2.setText("Load World 2");
        buttonWL2.setHorizontalTextPosition(JButton.CENTER);
        buttonWL2.setVerticalTextPosition(JButton.CENTER);
        buttonWL2.setFont(new Font("Arial", Font.BOLD, 50));

        buttonWL3 = new JButton();
        buttonWL3.addActionListener(e -> {inMainMenu = false; loadID = 3;});
        buttonWL3.setMargin(game.getInsets());
        buttonWL3.setText("Load World 3");
        buttonWL3.setHorizontalTextPosition(JButton.CENTER);
        buttonWL3.setVerticalTextPosition(JButton.CENTER);
        buttonWL3.setFont(new Font("Arial", Font.BOLD, 50));

        buttonME = new JButton();
        buttonME.addActionListener(e -> {System.out.println("Sorry, but this button has not been implemented yet!");});
        buttonME.setMargin(game.getInsets());
        buttonME.setText("Map Editor");
        buttonME.setHorizontalTextPosition(JButton.CENTER);
        buttonME.setVerticalTextPosition(JButton.CENTER);
        buttonME.setFont(new Font("Arial", Font.BOLD, 50));

        buttonNA1 = new JButton();
        buttonNA1.addActionListener(e -> {System.out.println("Sorry, but this button has not been implemented yet!");});
        buttonNA1.setMargin(game.getInsets());
        buttonNA1.setText("Reset World");
        buttonNA1.setHorizontalTextPosition(JButton.CENTER);
        buttonNA1.setVerticalTextPosition(JButton.CENTER);
        buttonNA1.setFont(new Font("Arial", Font.BOLD, 50));

        buttonGH = new JButton();
        buttonGH.addActionListener(e -> {System.out.println("go to: https://github.com/VAST-THE-DOGE/Tiles");});
        buttonGH.setMargin(game.getInsets());
        buttonGH.setText("Github");
        buttonGH.setHorizontalTextPosition(JButton.CENTER);
        buttonGH.setVerticalTextPosition(JButton.CENTER);
        buttonGH.setFont(new Font("Arial", Font.BOLD, 50));

        buttonS = new JButton();
        buttonS.addActionListener(e -> {System.out.println("Sorry, but this button has not been implemented yet!");});
        buttonS.setMargin(game.getInsets());
        buttonS.setText("Settings");
        buttonS.setHorizontalTextPosition(JButton.CENTER);
        buttonS.setVerticalTextPosition(JButton.CENTER);
        buttonS.setFont(new Font("Arial", Font.BOLD, 50));

        mainMenu.add(label);
        mainMenu.add(buttonME);
        mainMenu.add(buttonWL1);
        mainMenu.add(buttonNA1);
        mainMenu.add(buttonWL2);
        mainMenu.add(buttonGH);
        mainMenu.add(buttonWL3);
        mainMenu.add(buttonS);

        f.setLocation(new Point(400,150));

        f.setResizable(false);
        f.pack();
        f.setMinimumSize(new Dimension(750,500));
        f.setVisible(true);
        try {
            label.setIcon(new ImageIcon(backgroundImg.getScaledInstance(label.getWidth(), label.getHeight(), Image.SCALE_SMOOTH)));
            buttonWL1.setIcon(new ImageIcon(buttonImg.getScaledInstance(buttonWL1.getWidth(), buttonWL1.getHeight(), Image.SCALE_SMOOTH)));
            buttonWL2.setIcon(new ImageIcon(buttonImg.getScaledInstance(buttonWL2.getWidth(), buttonWL2.getHeight(), Image.SCALE_SMOOTH)));
            buttonWL3.setIcon(new ImageIcon(buttonImg.getScaledInstance(buttonWL3.getWidth(), buttonWL3.getHeight(), Image.SCALE_SMOOTH)));
            buttonME.setIcon(new ImageIcon(game.NO_IMAGE_ICON.getScaledInstance(buttonME.getWidth(), buttonME.getHeight(), Image.SCALE_SMOOTH)));
            buttonNA1.setIcon(new ImageIcon(game.NO_IMAGE_ICON.getScaledInstance(buttonNA1.getWidth(), buttonNA1.getHeight(), Image.SCALE_SMOOTH)));
            buttonGH.setIcon(new ImageIcon(game.NO_IMAGE_ICON.getScaledInstance(buttonGH.getWidth(), buttonGH.getHeight(), Image.SCALE_SMOOTH)));
            buttonS.setIcon(new ImageIcon(game.NO_IMAGE_ICON.getScaledInstance(buttonS.getWidth(), buttonS.getHeight(), Image.SCALE_SMOOTH)));

        } catch (Exception e) {
            System.err.println("icon update error");
        } 
        //menu loop
        while (inMainMenu) {
            try {
                Thread.sleep(100);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        f.setVisible(false);

        //in game loading setup

        //load map
        map = game.createTileMap(game.loadMap(loadID));

        //load resources
        playerResources = game.loadResources(loadID);

        //setup window
        f.setMinimumSize(new Dimension(1000,500));
        f.setResizable(true);
        p.setLayout(new GridLayout(20,40));
        p.setBackground(Color.BLACK);
        
        for(int j = 0; j < 20; j++)
        {
            for(int i = 0; i < 40; i++)
            {
                buttons[j][i] = new JButton();
                
                int column = i;
                int row = j;
                buttons[j][i].setBackground(Color.BLACK);
                buttons[j][i].addActionListener(e -> game.clicked(row,column));
                p.add(buttons[j][i]);
                buttons[j][i].setMargin(game.getInsets());
                buttons[j][i].setHorizontalTextPosition(JButton.CENTER);
                buttons[j][i].setVerticalTextPosition(JButton.CENTER);
                //buttons[j][i].setBorder(null);
            }
        }

        //new. update menu button stuff.
        for(int j = 17; j < 20; j++)
        {
            for(int i = 0; i < 40; i++)
            {
                if (true) {
                    buttons[j][i].setRolloverEnabled(false);
                    buttons[j][i].setBorder(null);
                }
            }
        }
        for(int j = 0; j < 17; j++)
        {
            for(int i = 33; i < 40; i++)
            {
                //buttons[j][i].setFocusPainted(false);
                buttons[j][i].setRolloverEnabled(false);
                buttons[j][i].setBorder(null);
            }
        }


        f.pack();

        f.setExtendedState (f.getExtendedState () | JFrame.MAXIMIZED_BOTH);
        
        f.setVisible(true);

        f.setContentPane(p);

        try {
            Thread.sleep(100);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        game.updateWindow(game.NO_IMAGE_ICON, p);

        double startTime = 0;

        //update player resource change
        resourceChange = game.getResourceChange();

        int oldHeight = f.getHeight();
        int oldWidth = f.getWidth();

        //game loop
        while(true)
        {
            try {
                Thread.sleep(50);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            if(speed != 0)
            {
            if(startTime < System.currentTimeMillis())
            {
                //manage time
                time[1] += 1;
                try {
                    startTime = System.currentTimeMillis()+(4000/(speed*speed));
                } catch (Exception e) {
                    startTime = System.currentTimeMillis()+1000;
                }
                
                if(time[1]>24)
                {
                    time[1] = 1;
                    time[0] += 1;

                    popDebuff += (time[0] - 1) * 2;

                    //update resources to be safe.
                    resourceChange = game.getResourceChange();
                }

            //check for window changes
                if(oldHeight!=f.getHeight() || oldWidth!=f.getWidth())
                {
                    //update tiles.
                    oldHeight = f.getHeight();
                    oldWidth = f.getWidth();

                    //create a new thread to update the window.
                    Runnable runnable = () -> {
                    game.updateWindow(game.NO_IMAGE_ICON, p);
                    };
                    Thread thread = new Thread (runnable);
                    thread.start ();
                    
                }

            //rest of stuff

            //update player resources
            for(int i = 0; i<7; i++)
            {
                playerResources[i] += resourceChange[i];
            }

            //check for resource values below 0, aka the if the player losses.
            for(int i = 0; i<7; i++)
            {
                if (playerResources[i] < 0) {
                    //call a lose function to display a screen, reset the world, and exit
                    //ToDo
                    System.exit(0);
                }
            }

            //save game, only if it is in save per day mode.
            if (savePerHour) {
                game.saveWorld(loadID);
            }
            else if (time[1] == 1) {
                game.saveWorld(loadID);
            }

            //Make a warning that highlights resources that are decreasing!
            if (resourceChange[0] < 0) {
                buttons[18][6].setForeground(Color.RED);
                buttons[18][7].setForeground(Color.RED);
                buttons[18][8].setForeground(Color.RED);
            }
            else {
                buttons[18][6].setForeground(Color.BLACK);
                buttons[18][7].setForeground(Color.BLACK);
                buttons[18][8].setForeground(Color.BLACK);
            }
            if (resourceChange[1] < 0) {
                buttons[18][11].setForeground(Color.RED);
                buttons[18][12].setForeground(Color.RED);
                buttons[18][13].setForeground(Color.RED);
            }
            else {
                buttons[18][11].setForeground(Color.BLACK);
                buttons[18][12].setForeground(Color.BLACK);
                buttons[18][13].setForeground(Color.BLACK);
            }
            if (resourceChange[2] < 0) {
                buttons[18][16].setForeground(Color.RED);
                buttons[18][17].setForeground(Color.RED);
                buttons[18][18].setForeground(Color.RED);
            }
            else {
                buttons[18][16].setForeground(Color.BLACK);
                buttons[18][17].setForeground(Color.BLACK);
                buttons[18][18].setForeground(Color.BLACK);
            }
            if (resourceChange[3] < 0) {
                buttons[18][21].setForeground(Color.RED);
                buttons[18][22].setForeground(Color.RED);
                buttons[18][23].setForeground(Color.RED);
            }
            else {
                buttons[18][21].setForeground(Color.BLACK);
                buttons[18][22].setForeground(Color.BLACK);
                buttons[18][23].setForeground(Color.BLACK);
            }
            if (resourceChange[4] < 0) {
                buttons[18][26].setForeground(Color.RED);
                buttons[18][27].setForeground(Color.RED);
                buttons[18][28].setForeground(Color.RED);
            }
            else {
                buttons[18][26].setForeground(Color.BLACK);
                buttons[18][27].setForeground(Color.BLACK);
                buttons[18][28].setForeground(Color.BLACK);
            }
            if (resourceChange[5] < 0) {
                buttons[18][31].setForeground(Color.RED);
                buttons[18][32].setForeground(Color.RED);
                buttons[18][33].setForeground(Color.RED);
            }
            else {
                buttons[18][31].setForeground(Color.BLACK);
                buttons[18][32].setForeground(Color.BLACK);
                buttons[18][33].setForeground(Color.BLACK);
            }
            if (resourceChange[6] < 0) {
                buttons[18][36].setForeground(Color.RED);
                buttons[18][37].setForeground(Color.RED);
                buttons[18][38].setForeground(Color.RED);
            }
            else {
                buttons[18][36].setForeground(Color.BLACK);
                buttons[18][37].setForeground(Color.BLACK);
                buttons[18][38].setForeground(Color.BLACK);
            }
            
            //print for debug
            System.out.println(time[0]+" Days and "+time[1]+" Hours.");

            //update the menu
            buttons[18][2].setText(""+time[0]);
            buttons[18][3].setText(time[1]+":00");
            String[] display = game.getResourceDisplay(playerResources[0]);
            buttons[18][6].setText(display[0]);
            buttons[18][7].setText(display[1]);
            buttons[18][8].setText(display[2]);
            display = game.getResourceDisplay(playerResources[1]);
            buttons[18][11].setText(display[0]);
            buttons[18][12].setText(display[1]);
            buttons[18][13].setText(display[2]);
            display = game.getResourceDisplay(playerResources[2]);
            buttons[18][16].setText(display[0]);
            buttons[18][17].setText(display[1]);
            buttons[18][18].setText(display[2]);
            display = game.getResourceDisplay(playerResources[3]);
            buttons[18][21].setText(display[0]);
            buttons[18][22].setText(display[1]);
            buttons[18][23].setText(display[2]);
            display = game.getResourceDisplay(playerResources[4]);
            buttons[18][26].setText(display[0]);
            buttons[18][27].setText(display[1]);
            buttons[18][28].setText(display[2]);
            display = game.getResourceDisplay(playerResources[5]);
            buttons[18][31].setText(display[0]);
            buttons[18][32].setText(display[1]);
            buttons[18][33].setText(display[2]);
            display = game.getResourceDisplay(playerResources[6]);
            buttons[18][36].setText(display[0]);
            buttons[18][37].setText(display[1]);
            buttons[18][38].setText(display[2]);

            //update upgrade buttons
            if (map[selected[0]][selected[1]].upgrades[0] != 0 && game.resourceCheck(map[selected[0]][selected[1]].cost1)) {
                buttons[9][38].setIcon(menuIcons[17]);
            }
            else {
                buttons[9][38].setIcon(menuIcons[16]);
            }
            if (map[selected[0]][selected[1]].upgrades[1] != 0 && game.resourceCheck(map[selected[0]][selected[1]].cost2)) {
                buttons[11][35].setIcon(menuIcons[17]);
            }
            else {
                buttons[11][35].setIcon(menuIcons[16]);
            }
            if (map[selected[0]][selected[1]].upgrades[2] != 0 && game.resourceCheck(map[selected[0]][selected[1]].cost3)) {
                buttons[11][38].setIcon(menuIcons[17]);
            }
            else {
                buttons[11][38].setIcon(menuIcons[16]);
            }

            }
            }
        }
        }

    private void clicked(int row, int column)
    {
        try{
        //is the button a tile in the map?
        if(row<17&&column<33)
        {
            //print debug stuff
            System.out.println("clicked");
            System.out.println(map[row][column]+" at ("+row+", "+column+").");

            //update the buttons
            buttons[selected[0]][selected[1]].setBackground(Color.BLACK);
            buttons[selected[0]][selected[1]].setIcon(tileIcons[map[selected[0]][selected[1]].id]);
            buttons[row][column].setIcon(new ImageIcon(map[row][column].imageFile.getScaledInstance((int)(buttons[0][0].getWidth()/1.2), (int)(buttons[0][0].getHeight()/1.2), Image.SCALE_SMOOTH)));
            buttons[row][column].setBackground(Color.RED);

            //update selected
            selected[0] = row;
            selected[1] = column;

            //update the tile upgrade menu
            buttons[1][34].setIcon(tileIcons[map[selected[0]][selected[1]].id]);
            if(map[selected[0]][selected[1]].upgrades[0] != 0)
            {buttons[9][37].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[0]]);}
            else buttons[9][37].setIcon(menuIcons[28]);
            if(map[selected[0]][selected[1]].upgrades[1] != 0)
            {buttons[11][34].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[1]]);}
            else buttons[11][34].setIcon(menuIcons[28]);
            if(map[selected[0]][selected[1]].upgrades[2] != 0)
            {buttons[11][37].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[2]]);}
            else buttons[11][37].setIcon(menuIcons[28]);

            String[] name = getNameDisplay(map[selected[0]][selected[1]].name);
            buttons[1][35].setText(name[0]);
            buttons[1][36].setText(name[1]);
            buttons[1][37].setText(name[2]);
            buttons[1][38].setText(name[3]);

            //update upgrade buttons
            if (map[selected[0]][selected[1]].upgrades[0] != 0 && resourceCheck(map[selected[0]][selected[1]].cost1)) {
                buttons[9][38].setIcon(menuIcons[17]);
            }
            else {
                buttons[9][38].setIcon(menuIcons[16]);
            }
            if (map[selected[0]][selected[1]].upgrades[1] != 0 && resourceCheck(map[selected[0]][selected[1]].cost2)) {
                buttons[11][35].setIcon(menuIcons[17]);
            }
            else {
                buttons[11][35].setIcon(menuIcons[16]);
            }
            if (map[selected[0]][selected[1]].upgrades[2] != 0 && resourceCheck(map[selected[0]][selected[1]].cost3)) {
                buttons[11][38].setIcon(menuIcons[17]);
            }
            else {
                buttons[11][38].setIcon(menuIcons[16]);
            }

            //update income
            buttons[3][35].setText(""+((map[selected[0]][selected[1]].ResourceLoss[0]*-1)+map[selected[0]][selected[1]].ResourceGain[0]));
            buttons[3][38].setText(""+((map[selected[0]][selected[1]].ResourceLoss[1]*-1)+map[selected[0]][selected[1]].ResourceGain[1]));
            buttons[5][35].setText(""+((map[selected[0]][selected[1]].ResourceLoss[2]*-1)+map[selected[0]][selected[1]].ResourceGain[2]));
            buttons[5][38].setText(""+((map[selected[0]][selected[1]].ResourceLoss[3]*-1)+map[selected[0]][selected[1]].ResourceGain[3]));
            buttons[7][35].setText(""+((map[selected[0]][selected[1]].ResourceLoss[4]*-1)+map[selected[0]][selected[1]].ResourceGain[4]));
            buttons[7][38].setText(""+((map[selected[0]][selected[1]].ResourceLoss[5]*-1)+map[selected[0]][selected[1]].ResourceGain[5]));
            buttons[9][35].setText(""+((map[selected[0]][selected[1]].ResourceLoss[6]*-1)+map[selected[0]][selected[1]].ResourceGain[6]));
        }

        //if not, it is a click on the menu.
        else
        {
            //print debug stuff
            System.out.println("clicked");
            System.out.println("Menu at ("+row+", "+column+").");
            if(row == 15)
            {
                switch (column) {
                    case 38: speed = 4; break;
                    case 37: speed = 3; break;
                    case 36: speed = 2; break;
                    case 35: speed = 1; break;
                    case 34: speed = 0; break;
                    default: break;
                }
                buttons[15][34].setIcon(menuIcons[23]);
                buttons[15][35].setIcon(menuIcons[24]);
                buttons[15][36].setIcon(menuIcons[25]);
                buttons[15][37].setIcon(menuIcons[26]);
                buttons[15][38].setIcon(menuIcons[27]);
                buttons[15][34+speed].setIcon(menuIcons[18+speed]);
            }
            else if (row == 9 && column == 38) {
                upgradeTile(row, column, map[selected[0]][selected[1]], 1);
                buttons[selected[0]][selected[1]].setBackground(Color.RED);
            }
            else if (row == 11 && column == 35) {
                upgradeTile(row, column, map[selected[0]][selected[1]], 2);
                buttons[selected[0]][selected[1]].setBackground(Color.RED);             
            }
            else if (row == 11 && column == 38) {
                upgradeTile(row, column, map[selected[0]][selected[1]], 3);
                buttons[selected[0]][selected[1]].setBackground(Color.RED);             
            }
            clicked(selected[0],selected[1]);
        }}
        catch (Exception e) {
            System.out.println("Click Error!");
        }
    }

    private void updateWindow(BufferedImage imageFile, JPanel panel)
    {
        try
        {
        //update tiles.
        for(int id = 0; id<=MAX_TILES; id++)
        {
            //default
            tileIcons[id] = new ImageIcon(imageFile.getScaledInstance(buttons[0][0].getWidth(), buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
            try {
                tileIcons[id] = new ImageIcon(ImageIO.read(getClass().getResourceAsStream("Data/ImageData/Tile"+id+".png")).getScaledInstance(buttons[0][0].getWidth(), buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
            }
            catch (IOException e)
            {
                e.printStackTrace();
            } catch (Exception e2) {System.err.println("Image read error!");}
            
        }
        for(int id = 0; id<=27; id++)
        {
            
            try {
            menuIcons[id] = new ImageIcon(ImageIO.read(getClass().getResourceAsStream("Data/ImageData/Menu"+id+".png")).getScaledInstance((int)buttons[0][0].getWidth(), (int)buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
            } catch (IOException e) {
            e.printStackTrace();
            } catch (Exception e2) {System.err.println("Image read error!");}
        }
        menuIcons[28] = new ImageIcon(NO_IMAGE_ICON.getScaledInstance(buttons[0][0].getWidth(), buttons[0][0].getHeight(), Image.SCALE_SMOOTH));
        try {
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
        }}
        catch (Exception e) {
            System.out.println("Tile Map Update Error!");
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

        panel.repaint();

        //update the exact buttons and values on the menu

        //get the font size
        //int fontSize = ((buttons[0][0].getWidth()+buttons[0][0].getHeight())/2)/3;
        int fontSize = (buttons[0][0].getWidth())/3; //testing version

        //time
        buttons[18][1].setText("Day");
        buttons[18][1].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][2].setText(""+time[0]);
        buttons[18][2].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][3].setText(time[1]+":00");
        buttons[18][3].setFont(new Font("Arial", Font.BOLD, fontSize));

        //resources

        //set the labels for each resource.
        buttons[18][5].setText("Gold");
        buttons[18][5].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][6].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][7].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][8].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][10].setText("Iron");
        buttons[18][10].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][11].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][12].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][13].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][15].setText("Stone");
        buttons[18][15].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[18][16].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][17].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][18].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][20].setText("Wood");
        buttons[18][20].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[18][21].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][22].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][23].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][25].setText("Water");
        buttons[18][25].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[18][26].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][27].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][28].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][30].setText("Food");
        buttons[18][30].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[18][31].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][32].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][33].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[18][35].setText("Workers");
        buttons[18][35].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.5)));
        buttons[18][36].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][37].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[18][38].setFont(new Font("Arial", Font.BOLD, fontSize));

        panel.repaint();

        //update the values for each
        String[] display = getResourceDisplay(playerResources[0]);
        buttons[18][6].setText(display[0]);
        buttons[18][7].setText(display[1]);
        buttons[18][8].setText(display[2]);
        display = getResourceDisplay(playerResources[1]);
        buttons[18][11].setText(display[0]);
        buttons[18][12].setText(display[1]);
        buttons[18][13].setText(display[2]);
        display = getResourceDisplay(playerResources[2]);
        buttons[18][16].setText(display[0]);
        buttons[18][17].setText(display[1]);
        buttons[18][18].setText(display[2]);
        display = getResourceDisplay(playerResources[3]);
        buttons[18][21].setText(display[0]);
        buttons[18][22].setText(display[1]);
        buttons[18][23].setText(display[2]);
        display = getResourceDisplay(playerResources[4]);
        buttons[18][26].setText(display[0]);
        buttons[18][27].setText(display[1]);
        buttons[18][28].setText(display[2]);
        display = getResourceDisplay(playerResources[5]);
        buttons[18][31].setText(display[0]);
        buttons[18][32].setText(display[1]);
        buttons[18][33].setText(display[2]);
        display = getResourceDisplay(playerResources[6]);
        buttons[18][36].setText(display[0]);
        buttons[18][37].setText(display[1]);
        buttons[18][38].setText(display[2]);

        //update the tile upgrade menu

        //update icons
        buttons[1][34].setIcon(tileIcons[map[selected[0]][selected[1]].id]);
        if(map[selected[0]][selected[1]].upgrades[0] != 0)
        {buttons[9][37].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[0]]);}
        else buttons[9][37].setIcon(menuIcons[28]);
        if(map[selected[0]][selected[1]].upgrades[1] != 0)
        {buttons[11][34].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[1]]);}
        else buttons[11][34].setIcon(menuIcons[28]);
        if(map[selected[0]][selected[1]].upgrades[2] != 0)
        {buttons[11][37].setIcon(tileIcons[map[selected[0]][selected[1]].upgrades[2]]);}
        else buttons[11][37].setIcon(menuIcons[28]);
        

        buttons[1][35].setIcon(menuIcons[12]);
        buttons[1][36].setIcon(menuIcons[14]);
        buttons[1][37].setIcon(menuIcons[14]);
        buttons[1][38].setIcon(menuIcons[13]);

        //update the name
        String[] name = getNameDisplay(map[selected[0]][selected[1]].name);
        buttons[1][35].setText(name[0]);
        buttons[1][35].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/0.9412)));
        buttons[1][36].setText(name[1]);
        buttons[1][36].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/0.9412)));
        buttons[1][37].setText(name[2]);
        buttons[1][37].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/0.9412)));
        buttons[1][38].setText(name[3]);
        buttons[1][38].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/0.9412)));
        
        buttons[3][34].setIcon(menuIcons[12]);
        buttons[3][35].setIcon(menuIcons[13]);
        buttons[3][37].setIcon(menuIcons[12]);
        buttons[3][38].setIcon(menuIcons[13]);

        buttons[5][34].setIcon(menuIcons[12]);
        buttons[5][35].setIcon(menuIcons[13]);
        buttons[5][37].setIcon(menuIcons[12]);
        buttons[5][38].setIcon(menuIcons[13]);

        buttons[7][34].setIcon(menuIcons[12]);
        buttons[7][35].setIcon(menuIcons[13]);
        buttons[7][37].setIcon(menuIcons[12]);
        buttons[7][38].setIcon(menuIcons[13]);

        buttons[9][34].setIcon(menuIcons[12]);
        buttons[9][35].setIcon(menuIcons[13]);

        buttons[9][38].setIcon(menuIcons[16]);
        buttons[11][38].setIcon(menuIcons[16]);
        buttons[11][35].setIcon(menuIcons[16]);

        //update the income values
        //set the labels for each resource.
        buttons[3][34].setText("Gold");
        buttons[3][34].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[3][35].setFont(new Font("Arial", Font.BOLD, fontSize));
    
        buttons[3][37].setText("Iron");
        buttons[3][37].setFont(new Font("Arial", Font.BOLD, fontSize));
        buttons[3][38].setFont(new Font("Arial", Font.BOLD, fontSize));
    
        buttons[5][34].setText("Stone");
        buttons[5][34].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[5][35].setFont(new Font("Arial", Font.BOLD, fontSize));

        buttons[5][37].setText("Wood");
        buttons[5][37].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[5][38].setFont(new Font("Arial", Font.BOLD, fontSize));
    
        buttons[7][34].setText("Water");
        buttons[7][34].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[7][35].setFont(new Font("Arial", Font.BOLD, fontSize));
    
        buttons[7][37].setText("Food");
        buttons[7][37].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.2)));
        buttons[7][38].setFont(new Font("Arial", Font.BOLD, fontSize));
    
        buttons[9][34].setText("Workers");
        buttons[9][34].setFont(new Font("Arial", Font.BOLD, (int)(fontSize/1.5)));
        buttons[9][35].setFont(new Font("Arial", Font.BOLD, fontSize));

        //update the speed buttons
        buttons[15][34].setIcon(menuIcons[23]);
        buttons[15][35].setIcon(menuIcons[24]);
        buttons[15][36].setIcon(menuIcons[25]);
        buttons[15][37].setIcon(menuIcons[26]);
        buttons[15][38].setIcon(menuIcons[27]);
        buttons[15][34+speed].setIcon(menuIcons[18+speed]);

        //use this to update the window quickly.
        panel.repaint();

        }
        catch(Exception e)
        {
            System.err.println("!!!MAP UPDATE ERROR!!!");
        }
    }

    private String[] getNameDisplay(String string) {
        if (string == null) {
            String[] returnString = {"N","U","L","L"};
            return returnString;
        }
        String[] spaces = {"  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  "}; //20 total, 5 per menu tile
        char[] charArray = string.toCharArray();
        int j = 10 - charArray.length/2;
        String[] returnString = {"","","",""};
        for (int i = 0; i < charArray.length; i++) {
            spaces[j++] = ""+charArray[i];
        }
        int k = 0;
        for (int i = 0; i < 4; i++) {
            for (j = 0; j < 5; j++) {
                returnString[i] += spaces[k++];
            }
        }
        return returnString;
    }

    private String[] getResourceDisplay(int value)
    {
        String[] display = {"000,0","00,00","0,000"};
        if(value>999999999999d)
        {
            display[0] = "999,9";
            display[1] = "99,99";
            display[2] = "9,999";
        }
        else if(value>=0)
        {
            char[] nums = {'0','0','0','0','0','0','0','0','0','0','0','0'};
            char[] stringNums = (""+value).toCharArray();

            for(int i = 0; i<stringNums.length; i++)
            {
                nums[11-i] = stringNums[stringNums.length-i-1];
            }

            display[0] = ""+nums[0]+nums[1]+nums[2]+","+nums[3];
            display[1] = ""+nums[4]+nums[5]+","+nums[6]+nums[7];
            display[2] = ""+nums[8]+","+nums[9]+nums[10]+nums[11];
        }
        else 
        {
            display[0] = "null";
            display[1] = "null";
            display[2] = "null";
        }
        return display;
    }

    private int[] getResourceChange()
    {
        int[] netGain = {0,0,0,0,0,0,0};
        int[] netLoss = {0,0,0,0,0,0,0};
        int[] netChange = {0,0,0,0,0,0,0};
        for(int j = 0; j < 17; j++)
        {
            for(int i = 0; i < 33; i++)
            {
                for(int k = 0; k<7; k++)
                {
                netGain[k] += map[j][i].ResourceGain[k];
                netLoss[k] += map[j][i].ResourceLoss[k];
                }
            }
        }
        for(int i = 0; i<7; i++)
        {
            netChange[i] = netGain[i] - netLoss[i];
        }

        netChange[6] -= popDebuff;

        return netChange;
    }

    private void saveWorld(int ID) {
        System.out.println("Saving to world "+ID);
        //get the string
        String string = "#resources\n#index = resource. 0= gold  1= iron  2= stone  3= wood  4= water  5= food  6= population.\n";
        for (int i = 0; i < 7; i++) {
            string += playerResources[i] + " ";
        }
        string += "\n#time {day, hour}\n"+time[0]+" "+time[1]+"\n#map\n";
        for(int j = 0; j < 17; j++)
        {
            for(int i = 0; i < 33; i++)
            {
                string += map[j][i].id + " ";
            }
            string += "\n";
        }
        string += "#";
        //System.out.println(string);



        //save
        try {
            FileWriter writer = new FileWriter(new File(getClass().getResource("SavedWorlds/World"+ID+".txt").toURI()), StandardCharsets.UTF_8, false); // true to append. false to overwrite.
            BufferedWriter out = new BufferedWriter(writer);
            out.write(string.toCharArray());
            out.close();

            System.out.println("Saved!");

        } catch (Exception e) {
            System.out.println("Saving Error");
            //e.printStackTrace();
        }
        
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

    private int[] loadResources(int ID)
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

        //ToDo player resources for loop!
        for (int i = 0; i < 7; i++) {
            playerResources[i] = scanner.nextInt();
        }

        scanner.nextLine();
        scanner.nextLine();
        //load time
        time[0] = scanner.nextInt();
        time[1] = scanner.nextInt();
        scanner.nextLine();
        scanner.nextLine();
        //recalculate the popDebuff
        for (int i = 1; i < time[0]; i++) {
            popDebuff += (i - 1) * 2;
        }

        for(int i = 0; i<17; i++)
        {
            for(int j = 0; j<33; j++)
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
                newID = T.upgrades[0];
                break;

            case 2:
                cost = T.cost2;
                newID = T.upgrades[1];
                break;
                

            case 3:
                cost = T.cost3;
                newID = T.upgrades[2];
                break;
        }
        if (newID == 0) {
            return;
        }
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
        //update resource gain
        resourceChange = getResourceChange();
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
        Integer[] upgrades = {0,0,0};
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
            } catch (Exception e2) {System.err.println("Image read error!");}
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
                        upgrades[i] = scanner.nextInt();
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
        public String toString()
        {return "Tile: "+ id;}
    }    
}
