<template>
  <div class="vehicePrice">
    <v-card elevation="4" class="vehicePrice__card">
      <v-card variant="tonal" class="price">
        <span class="text-h4">
          <v-icon class="icon" size="40" icon="mdi-cash-multiple" />
          <span>{{ price?.Value }}</span>
        </span>
      </v-card>

      <span class="item">
        <v-icon class="icon" size="24" icon="mdi-domain" /> Brand:
        <strong>{{ price?.BrandName }}</strong>
      </span>
      <span class="item">
        <v-icon class="icon" size="24" icon="mdi-car" /> Model:
        <strong>{{ price?.ModelName }}</strong>
      </span>
      <span class="item">
        <v-icon class="icon" size="24" icon=" mdi-calendar-star-four-points" />
        Model year:
        <strong>{{ price?.ModelYear }}</strong>
      </span>
      <span class="item">
        <v-icon class="icon" size="24" icon="mdi-fuel" /> Fuel:
        <strong>{{ price?.Fuel }}</strong>
      </span>
      <span class="item fipe">
        <span>
          Fipe code: <strong class="code">{{ price?.FipeCode }}</strong>
        </span>
        <span class="reference">
          Reference month: <strong>{{ price?.ReferenceMonth }}</strong>
        </span>
      </span>
    </v-card>
  </div>
</template>

<script setup>
import { useFipeConsumerStore } from '@/store/FipeConsumerStore';
import { storeToRefs } from 'pinia';

const fipeStore = useFipeConsumerStore();
const { price } = storeToRefs(fipeStore);
</script>

<style lang="scss" scoped>
.vehicePrice {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;

  &__card {
    display: flex;
    align-items: flex-start;
    justify-content: center;
    gap: 10px;
    flex-flow: column;
    padding: 15px;
    width: 100%;
    background-color: #eee;

    .price {
      width: 100%;
      padding: 8px;
      background-color: lightgreen;
      margin-bottom: 10px;

      > span {
        color: forestgreen;
        font-weight: bold;
        display: flex;
        align-items: center;
        justify-content: flex-start;
        gap: 15px;

        .icon {
          margin-top: -10px;
        }
      }
    }

    .item {
      width: 100%;
      color: #444;

      .icon {
        margin-top: -5px;
        color: #999;
      }

      &.fipe {
        margin-top: 10px;
        font-size: 14px;
        color: #666;
        display: flex;
        flex-flow: column;
        align-items: flex-end;

        .code {
          color: #2167c0;
        }

        .reference {
          font-size: 10px;
        }
      }
    }
  }
}
</style>
