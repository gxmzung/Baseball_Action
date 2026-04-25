package com.citybrain.baseball.player;

public class Player {
    public String id;
    public String name;
    public String position;
    public int age;
    public int overall;
    public int contact;
    public int power;
    public int speed;
    public int defense;
    public int throwing;
    public int marketValue;

    public Player(String id, String name, String position, int age, int overall, int contact, int power, int speed, int defense, int throwing, int marketValue) {
        this.id = id;
        this.name = name;
        this.position = position;
        this.age = age;
        this.overall = overall;
        this.contact = contact;
        this.power = power;
        this.speed = speed;
        this.defense = defense;
        this.throwing = throwing;
        this.marketValue = marketValue;
    }
}
