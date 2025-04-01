<template>
  <div>
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

<script setup lang="ts"></script>

<style scoped>
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
