const animals = [
    { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
    { name: "Carlo", species: "dog", class: { name: "vertebrata" } },
    { name: "Nemo", species: "fish", class: { name: "pisces" } },
    { name: "Hamilton", species: "dog", class: { name: "vertebrata" } },
    { name: "Dory", species: "fish", class: { name: "pisces" } },
    { name: "Ursa", species: "cat", class: { name: "mamalia" } },
    { name: "Taro", species: "cat", class: { name: "mamalia" } }
];

var find = animals.filter(function (cari) {
    return cari.species == "cat";
});
console.log(find);

for (let x = 0; x < animals.length; x++) {
    if (animals[x].class.name != "mamalia") {
        animals[x].class.name = "non-mamalia";
    }
    console.log(animals[x]);
}