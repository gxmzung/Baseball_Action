package com.citybrain.baseball.player;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;

@RestController
public class PlayerController {
    @GetMapping("/api/players")
    public List<Player> players() {
        return List.of(
            new Player("p001", "Kim Ace", "SP", 24, 78, 20, 25, 30, 55, 60, 350000),
            new Player("p002", "Lee Slugger", "1B", 26, 81, 73, 90, 45, 58, 61, 420000),
            new Player("p003", "Park Speedster", "CF", 22, 76, 69, 52, 92, 84, 70, 280000),
            new Player("p004", "Choi Cannon", "RF", 25, 84, 77, 94, 61, 70, 88, 620000)
        );
    }
}
