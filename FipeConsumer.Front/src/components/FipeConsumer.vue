<template>
  <div>
    <h2>Marcas FIPE</h2>
    <input v-model="filter" placeholder="Digite para filtrar as marcas..." />
    <table>
      <thead>
        <tr>
          <th>Código</th>
          <th>Nome</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="brand in filteredBrands" :key="`${brand?.nome}`">
          <td>{{ brand.codigo }}</td>
          <td>{{ brand.nome }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { useFipeBrands } from "@/composables/useFipeBrands";

const { filter, filteredBrands } = useFipeBrands();
</script>

<style scoped>
input {
  margin-bottom: 1rem;
  padding: 8px;
  width: 100%;
  max-width: 400px;
  box-sizing: border-box;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  padding: 8px;
  border: 1px solid #ddd;
  text-align: left;
}
</style>
