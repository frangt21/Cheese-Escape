using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para que sea fácil de acceder desde otros scripts.
    public static GameManager instance;

    private int collectedCheeses = 0;
    private int cheesesToWin = 5;

    void Awake()
    {
        // Configuración del Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // El jugador llamará a esta función cada vez que recoja un queso.
    public void AddCheese()
    {
        collectedCheeses++;
        Debug.Log("Quesos recogidos: " + collectedCheeses); // Para ver el progreso en la consola.

        // Si hemos recogido los necesarios, ganamos.
        if (collectedCheeses >= cheesesToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("¡HAS GANADO!");

        // Buscamos todos los objetos con el tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // Y los destruimos
            Destroy(enemy);
        }

        // Después de 2 segundos de "pausa" para que veas que ganaste, reinicia el juego.
        // Invoke es una forma sencilla de llamar a una función después de un tiempo.
        Invoke("RestartGame", 2f); 
    }

    // El enemigo llamará a esta función cuando atrape al jugador.
    public void LoseGame()
    {
        Debug.Log("GAME OVER");
        RestartGame(); // Reinicia inmediatamente.
    }

    // Función para reiniciar la escena.
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}