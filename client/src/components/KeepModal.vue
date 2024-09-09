<template>
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="keepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div v-if="keep" class="modal-content">

                <div class="modal-body">
                    <img :src="keep.img" :alt="keep.name" class="img">
                    <h2 class="text-center">{{ keep.name }}</h2>
                    <p>{{ keep.description }}</p>

                    <p>Views:{{ keep.views }} Kept:{{ keep.kept }} </p>


                    <RouterLink class="selectable" data-dismiss="keepModal"
                        :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">

                        <img :src="keep.creator.picture" :alt="keep.creator.name" class="creatorImg">

                    </RouterLink>
                    <!-- <div v-if="account.id == keep.creatorId">

                        <button @click="deleteKeep(keep.id)" type="button" class="btn btn-primary">DELETE</button>

                    </div> -->
                </div>
                <div class="modal-footer">


                    <form action="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Add Keep to Vault
                            </button>

                            <ul class="dropdown-menu">
                                <div class="form-select" aria-label="Select a Vault" required>
                                    <li v-for="vault in vaults" :key="vault.id" :value="vault.id" class="dropdown-item"
                                        @click="createVaultKeep(vault)">
                                        {{ vault.name }}
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form>

                    <!-- <form action="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Dropdown button
                            </button>

                            <ul class="dropdown-menu">
                                <div class="form-select" aria-label="Select a Vault" required>
                                    <li v-for="vault in vaults" :key="vault.id" :value="vault.id" class="dropdown-item"
                                        @click="deleteVaultKeep(vault)">
                                        {{ vault.name }}
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form> -->

                    <!-- <button v-if="account.id" @click="createVaultKeep(activeKeep.id)" type="button"
                        class="btn btn-primary">Add to Vault</button> -->

                    <!-- <VaultListComponent /> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
import { Vault } from '../models/Vault';
import { Account } from '../models/Account';
import VaultListComponent from '../components/VaultListComponent.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { logger } from '../utils/Logger';



export default {

    props: {
        account: { type: Account },

    },
    setup() {

        const editableVaultKeepData = ref({})
        // onMounted(getVaults)
        const activeKeep = computed(() => AppState.activeKeep)



        return {
            editableVaultKeepData,
            vaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
            keep: computed(() => AppState.activeKeep),
            activeKeep,
            // activeVault,
            async createVaultKeep(vault) {
                try {
                    logger.log('vault id:', vault.id)
                    logger.log('keep Id', activeKeep.value.id)



                    const vaultKeepData = {
                        vaultId: vault.id,
                        keepId: activeKeep.value.id
                    }

                    await vaultKeepsService.createVaultKeep(vaultKeepData)
                }
                catch (error) {
                    Pop.error(error);
                }
            },

            // async deleteVaultKeep(vaultKeepId) {
            //     try {
            //         const yes = await Pop.confirm()
            //         if (!yes) return
            //         await vaultKeepsService.deleteVaultKeep(vaultKeepId)
            //     } catch (error) {
            //         Pop.error(error);
            //     }
            // }


            // getVaults() {
            //     try {
            //         vaultsService.getVaults()
            //     }
            //     catch (error) {
            //         Pop.error(error);
            //     }
            // }

        }
    },
    components: { VaultListComponent }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}

.creatorImg {
    width: 50%;
    border-radius: 100px;
    object-fit: cover;
    height: 20vh;
}
</style>