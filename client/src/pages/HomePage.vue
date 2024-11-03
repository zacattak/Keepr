<template>
  <div class="container-fluid">
    <section class="row">
      <div class="mx-auto masonry">
        <div v-for="keep in paginatedKeeps" :key="keep.id" class="">
          <KeepComponent :keep="keep" />
        </div>
      </div>
    </section>


    <div class="pagination-controls">
      <button @click="prevPage" :disabled="currentPage === 1">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">Next</button>
    </div>
  </div>
</template>

<script>
import { computed, ref, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService.js';

export default {
  setup() {
    const currentPage = ref(1);
    const keepsPerPage = 20;

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error);
      }
    }

    onMounted(getKeeps);

    const totalPages = computed(() => Math.ceil(AppState.keeps.length / keepsPerPage));
    const paginatedKeeps = computed(() => {
      const start = (currentPage.value - 1) * keepsPerPage;
      const end = start + keepsPerPage;
      return AppState.keeps.slice(start, end);
    });

    function nextPage() {
      if (currentPage.value < totalPages.value) {
        currentPage.value++;
      }
    }

    function prevPage() {
      if (currentPage.value > 1) {
        currentPage.value--;
      }
    }

    return {
      keeps: computed(() => AppState.keeps),
      paginatedKeeps,
      currentPage,
      totalPages,
      nextPage,
      prevPage
    };
  }
};
</script>

<style scoped lang="scss">
.container-fluid {
  background-color: #dcdcdc;
  min-height: 100vh;
}

.card {
  border: 2px solid black;
  border-radius: 16px;
  box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

.masonry {
  column-count: 3;
  column-gap: 1rem;
}

.masonry>div {
  break-inside: avoid;
  margin-bottom: 1rem;
}

.pagination-controls {
  display: flex;
  justify-content: center;
  margin-top: 1rem;
}

.pagination-controls button {
  margin: 0 0.5rem;
}

/* Responsive Masonry Layout */
@media (max-width: 768px) {
  .masonry {
    column-count: 2;
  }
}

@media (max-width: 576px) {
  .masonry {
    column-count: 1;
  }
}
</style>
