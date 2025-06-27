document.addEventListener('DOMContentLoaded', async () => {
    const continentsDiv = document.getElementById('continents-data');
    const errorMessageDiv = document.getElementById('error-message');
    const mlyContainer = document.getElementById('mly');

    const displayError = (message) => {
        errorMessageDiv.textContent = message;
        errorMessageDiv.style.display = 'block';
        continentsDiv.innerHTML = '';
    };

    try {
        errorMessageDiv.style.display = 'none';
        const response = await fetch('/api/Geo/continents');

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Błąd HTTP! Status: ${response.status}. Treść: ${errorText}`);
        }

        const data = await response.json();
        console.log("Pobrane kontynenty:", data);

        if (data.length === 0) {
            continentsDiv.textContent = "Brak kontynentów w bazie. Dodaj je przez Swagger UI (dostępny pod adresem głównym API, np. https://localhost:7197/)!";
        } else {
            continentsDiv.innerHTML = '<ul>' + data.map(c => `<li>${c.name}</li>`).join('') + '</ul>';
        }

    } catch (error) {
        console.error("Błąd podczas pobierania danych kontynentów:", error);
        displayError(`Wystąpił błąd podczas pobierania kontynentów: ${error.message}. Sprawdź, czy API działa poprawnie i czy dane istnieją.`);
    }


    const mapContainer = document.getElementById('map');
    if (!mapContainer) {
        console.error("Element #map nie został znaleziony. Mapa nie może zostać zainicjalizowana.");
        return;
    }

    console.log("Inicjalizacja mapy...");
    var map = L.map('map').setView([20, 0], 2); 
    console.log("Mapa zainicjalizowana!");

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    const MAPILLARY_CLIENT_ID = '746862837801607'; 
    const mapillaryLayer = L.mapillary(MAPILLARY_CLIENT_ID, {
        layerType: 'point' 
    }).addTo(map);

    var countries = [
        { name: "Polska", lat: 52.2297, lon: 21.0122 },
        { name: "Niemcy", lat: 52.52, lon: 13.405 },
        { name: "Francja", lat: 48.8566, lon: 2.3522 }
    ];

    countries.forEach(c => {
        L.marker([c.lat, c.lon])
            .addTo(map)
            .bindPopup(`<b>${c.name}</b><br><button onclick="showMapillary(${c.lat}, ${c.lon})">Pokaż Widok Ulicy</button>`);
    });

    window.showMapillary = async (lat, lon) => {
        if (!mlyContainer) {
             console.error("Element #mly nie został znaleziony.");
             alert('Kontener widoku Mapillary nie jest dostępny.');
             return;
        }

        continentsDiv.style.display = 'none';
        mlyContainer.style.display = 'block';

        if (viewer) {
            viewer.remove();
            mlyContainer.innerHTML = '';
        }

        viewer = new Mapillary.Viewer({
            container: 'mly',
            accessToken: MAPILLARY_CLIENT_ID,
            imageId: null, 
            component: {
                cover: false
            }
        });

        try {
            const response = await fetch(`https://graph.mapillary.com/images?at=${lon},${lat}&client_id=${MAPILLARY_CLIENT_ID}&access_token=${MAPILLARY_CLIENT_ID}&fields=id`);
            const data = await response.json();

            if (data.data && data.data.length > 0) {
                const imageId = data.data[0].id;
                viewer.moveTo(imageId); 
                mlyContainer.scrollIntoView({ behavior: 'smooth' });
            } else {
                alert('Brak dostępnych zdjęć Mapillary dla tej lokalizacji.');
                mlyContainer.innerHTML = '';
                mlyContainer.style.display = 'none'; 
                continentsDiv.style.display = 'block'; 
            }
        } catch (error) {
            console.error('Błąd podczas pobierania danych Mapillary:', error);
            alert('Wystąpił błąd podczas ładowania widoku Mapillary.');
            mlyContainer.innerHTML = ''; 
            mlyContainer.style.display = 'none'; 
            continentsDiv.style.display = 'block'; 
        }
    };
});