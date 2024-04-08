<template>
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="keepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div v-if="activeKeep" class="modal-content">
                <!-- <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div> -->
                <div class="modal-body">
                    <img :src="activeKeep.img" :alt="activeKeep.name" class="img">
                    <h2 class="text-center">{{ activeKeep.name }}</h2>
                    <p>{{ activeKeep.description }}</p>

                    <p>Views:{{ activeKeep.views }} Kept:{{ activeKeep.kept }}</p>
                    <!-- <ul v-for="ingredient in recipeIngredients" :key="ingredient.id">
                        {{ ingredient.name }} | {{ ingredient.quantity }}
                    </ul> -->

                </div>
                <div class="modal-footer">
                    <!-- 
                    <button v-if="!isFavorite && account.id" @click="createVaultKeep(activeKeep.id)" type="button"
                        class="btn btn-primary">Add to Vault</button> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';


export default {
    setup() {
        const activeKeep = computed(() => AppState.activeKeep)
        return {
            // account: computed(() => AppState.account),
            activeKeep
        }
    }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}
</style>