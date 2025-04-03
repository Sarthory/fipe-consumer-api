<template>
  <div class="fipeApplication">
    <header class="fipeApplication__header">
      <img src="./assets/fipe-logo.png" alt="Fipe logo" width="90" />
      <h2>Brazilian car prices table based on Fipe API</h2>
    </header>
    <main class="fipeApplication__main">
      <div class="mainContainer">
        <router-view />
      </div>
    </main>
    <footer class="fipeApplication__footer">
      <span>Felipe Sartori - {{ new Date().getFullYear() }}</span>
    </footer>
  </div>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped>
.fipeApplication {
  display: flex;
  flex-direction: column;
  min-height: 100vh;

  &__header {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 20px;
    background-color: #ddd;
    color: #444;
    padding: 8px;
    z-index: 2;
    box-shadow: 0px 2px 8px 2px #999;
  }

  &__main {
    display: flex;
    align-items: flex-start;
    justify-content: center;
    flex: 1;
    background-color: #fafafa;

    .mainContainer {
      padding: 20px;
      max-width: 800px;
    }
  }

  &__footer {
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: #ddd;
    color: #444;
    padding: 4px;
  }
}
</style>
