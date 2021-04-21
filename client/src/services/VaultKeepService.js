/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepService {
  async createRelationship(vaultId, keepId) {
    try {
      const newRelationship = {
        vaultId: vaultId,
        keepId: keepId,
        creatorId: AppState.account.id
      }
      await api.post('api/vaultkeeps', newRelationship)
    } catch (err) {
      logger.error('Couldnt create VaultKeep', err)
    }
  }

  async deleteRelationship(vaultkeepId) {
    try {
      if (window.confirm('Are you sure you want to remove this from your vaultkeep?')) {
        await api.delete('api/vaultkeepkeeps/' + vaultkeepKeepId)
        this.getVaultKeepKeeps()
      }
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultkeepService = new VaultKeepService()
