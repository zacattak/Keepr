<template>
    <div>
        <input type="text" v-model="tagName" placeholder="Search by tag name" @keyup.enter="searchKeeps" />
        <button @click="searchKeeps">Search</button>

        <div class="masonry">
            <div v-for="keep in searchResults" :key="keep.id">
                <KeepComponent :keep="keep" />
            </div>
        </div>
    </div>
</template>

<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService';

export default {
    setup() {
        const tagName = ref('');
        const searchResults = ref([]);

        async function searchKeeps() {
            if (tagName.value) {
                searchResults.value = await keepsService.searchKeepsByTag(tagName.value);
            }
        }

        return {
            tagName,
            searchResults,
            searchKeeps
        };
    }
};
</script>

<style scoped>
.masonry {
    column-count: 3;
    column-gap: 1rem;
}
</style>