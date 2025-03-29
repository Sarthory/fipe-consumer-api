<!-- src/App.vue -->
<template>
  <div id="app">
    <header>
      <h1>Minha Aplicação FIPE</h1>
    </header>
    <main>
      <router-view />
    </main>
    <footer>
      <p>&copy; 2023 Minha Empresa</p>
    </footer>
  </div>
</template>

<script setup lang="ts">
// Você pode colocar aqui lógicas globais ou imports necessários
</script>

<style scoped>
/* Estilos globais ou específicos do App */
#app {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
header,
footer {
  background-color: #f5f5f5;
  padding: 1rem;
}
main {
  flex: 1;
  padding: 1rem;
}
</style>
