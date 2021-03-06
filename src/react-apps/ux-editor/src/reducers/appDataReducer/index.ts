import { combineReducers, Reducer } from 'redux';
import appConfigReducer, { IAppConfigState } from './appConfigReducer';
import codeListsReducer, { ICodeListsState } from './codeListsReducer';
import dataModelReducer, { IDataModelState } from './dataModelReducer';
import ruleModelReducer, { IRuleModelState } from './ruleModelReducer';
import textResourceReducer, { ITextResourcesState } from './textResourcesReducer';

export interface IAppDataState {
  appConfig: IAppConfigState;
  dataModel: IDataModelState;
  textResources: ITextResourcesState;
  ruleModel: IRuleModelState;
  codeLists: ICodeListsState;
}

const combinedReducers: Reducer<IAppDataState> = combineReducers({
  appConfig: appConfigReducer,
  dataModel: dataModelReducer,
  textResources: textResourceReducer,
  ruleModel: ruleModelReducer,
  codeLists: codeListsReducer,
});

export default combinedReducers;
