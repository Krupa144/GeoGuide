// Prosty przykład: kliknięcie w kraj → przekierowanie
const map = L.map('map', {
    dragging: false // This line disables all dragging (panning)
}).setView([20, 0], 2);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
  attribution: '&copy; OpenStreetMap contributors',
  noWrap: true // Still good to keep this for tile repetition
}).addTo(map);

function onEachFeature(feature, layer) {
  if (feature.properties && feature.properties.name) {
    layer.bindTooltip(feature.properties.name);
    layer.on('click', () => {
      const country = feature.properties.name;
      window.location.href = `region.html?country=${encodeURIComponent(country)}`;
    console.log("test");
    });

  }
}

fetch("geo/countries.geojson")
  .then(res => res.json())
  .then(data => {
    L.geoJSON(data, {
      onEachFeature: onEachFeature,
      style: { color: "#3388ff", weight: 1 }
    }).addTo(map);
  });
map.setMinZoom(2);
map.setMaxZoom(18)

 