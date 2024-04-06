namespace Keepr.Models;

public class Vault
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatorId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Img { get; set; }

    public bool IsPrivate { get; set; }

    public Account Creator { get; set; }
}

// CREATE TABLE vaults (
//     id INT PRIMARY KEY AUTO_INCREMENT NOT NULL, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', creatorId VARCHAR(255) NOT NULL, name VARCHAR(255) NOT NULL, description VARCHAR(1000) NOT NULL, img VARCHAR(1000) NOT NULL, isPrivate BOOLEAN NOT NULL DEFAULT false, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
// )